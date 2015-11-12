using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ChemSpiderClient.ChemSpiderSearch;
using ChemSpiderClient.Properties;
using System.ComponentModel;

namespace ChemSpiderClient
{
    public enum SearchType
    {
        Elements,
        IntrinsicProperty,
        Lasso,
        PredictedProperty,
        Similarity,
        Simple,
        ExactStructure,
        SubStructure
    } ;

    public enum TransferType
    {
        All,
        AllChemistry,
        AllInChI,
        Smiles,
        InChI,
        InChIKey,
        MolfileString,
        ThumbNail,
        None
    } ;

    public class Search
    {
        private readonly Form _owner;

        private readonly String _token;

        private readonly ChemSpiderSearch.Search _cs;

        private readonly ChemSpiderInChI.InChI _ci;

        private string _searchType;

        private bool _errorState;

        private bool _searchCancelled;

        private BackgroundWorker _bgwGetResults = null;

        private frmProgress _frmProgress;

        private readonly TransferType _transferType;

        private readonly bool _closeOnTransfer;

        public event TransferHandler StructureTransfered;

        public delegate void TransferHandler(object sender, StructureTransferEventsArgs e);

        #region properties
        public int[] Results { get; private set; }
        public ChemSpiderSearch.Search CsSearch { get { return _cs; } }
        #endregion

        #region constructor
        public Search(string token, IWin32Window owner, TransferType transferType, bool closeOnTransfer)
        {
            _owner = (Form)owner;
            _token = token;
            _transferType = transferType;
            _closeOnTransfer = closeOnTransfer;

            if (!string.IsNullOrEmpty(token))
            {

                // instantiate our ChemSpider Search instance
                _cs = new ChemSpiderSearch.Search();

                // instantiate our ChemSpider InChI instance
                _ci = new ChemSpiderInChI.InChI();

                // setup event handlers
                _cs.ElementsSearchCompleted += (cs_ElementsSearchCompleted);
                _cs.IntrinsicPropertiesSearchCompleted += (cs_IntrinsicPropertiesSearchCompleted);
                _cs.LassoSearchCompleted += (cs_LassoSearchCompleted);
                _cs.PredictedPropertiesSearchCompleted += (cs_PredictedPropertiesSearchCompleted);
                _cs.SimilaritySearchCompleted += (cs_SimilaritySearchCompleted);
                _cs.SimpleSearchCompleted += (cs_SimpleSearchCompleted);
                _cs.StructureSearchCompleted += (cs_StructureSearchCompleted);
                _cs.SubstructureSearchCompleted += (cs_SubstructureSearchCompleted);
                _cs.GetAsyncSearchResultCompleted += (_cs_GetAsyncSearchResultCompleted);
                _cs.GetAsyncSearchResultPartCompleted += (_cs_GetAsyncSearchResultPartCompleted);
            }
            else
            {
                MessageBox.Show(owner, "ChemSpider token not set", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Search Methods

        public void SetCommonSearchOptions()
        {
            var frmSetCommonSearchOptions = new frmCommonSearchOptions();
            frmSetCommonSearchOptions.ShowDialog(_owner);
        }

        public static string SetToken(string token, IWin32Window owner)
        {
            var frmToken= new frmToken(token);
            if (frmToken.ShowDialog(owner) == DialogResult.OK)
            {
                return frmToken.Token;
            }
            else
            {
                return token;
            }
        }

        private static void SetCommonSearchOptions(IDictionary<string, object> options)
        {
            //Common Search Options

            //Complexity
            options["Complexity"] = Settings.Default.Complexity;
            //Isotopic
            options["Isotopic"] = Settings.Default.Isotopic;
            //HasPatents
            options["HasPatents"] = Settings.Default.HasPatents;
            //HasSpectra
            options["HasSpectra"] = Settings.Default.HasSpectra;
        }

        public Dictionary<string, object> SetPropertySearchOptions(SearchType searchType, Dictionary<string, object> settings, out bool cancel)
        {
            SetCommonSearchOptions(settings);
            cancel = false;

            switch (searchType)
            {
                case (SearchType.IntrinsicProperty):
                    {
                        var fields = new[] { "EmpiricalFormula", "MolWeightMin", "MolWeightMax", "AverageMassMin", "AverageMassMax", "MonoisotopicMassMin", "MonoisotopicMassMax", "NominalMassMin", "NominalMassMax" };

                        foreach (var field in fields)
                        {
                            if (!settings.ContainsKey(field))
                            {
                                settings[field] = "";
                            }

                        }

                        var frmIntrinsicPropertySearch = new frmIntrinsicPropertySearch
                                                             {
                                                                 txtEmpiricalFormula =
                                                                     {Text = settings["EmpiricalFormula"].ToString()},
                                                                 ntMolWeightMin =
                                                                     {Text = settings["MolWeightMin"].ToString()},
                                                                 ntMolWeightMax =
                                                                     {Text = settings["MolWeightMax"].ToString()},
                                                                 ntAverageMassMin =
                                                                     {Text = settings["AverageMassMin"].ToString()},
                                                                 ntAverageMassMax =
                                                                     {Text = settings["AverageMassMax"].ToString()},
                                                                 ntMonoisotopicMassMin =
                                                                     {Text = settings["MonoisotopicMassMin"].ToString()},
                                                                 ntMonoisotopicMassMax =
                                                                     {Text = settings["MonoisotopicMassMax"].ToString()},
                                                                 ntNominalMassMin = 
                                                                     {Text = settings["NominalMassMin"].ToString()},
                                                                 ntNominalMassMax = 
                                                                     {Text = settings["NominalMassMax"].ToString()}
                                                             };

                        if (frmIntrinsicPropertySearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.txtEmpiricalFormula.Text))
                                settings["EmpiricalFormula"] = frmIntrinsicPropertySearch.txtEmpiricalFormula.Text;

                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntMolWeightMin.Text))
                                settings["MolWeightMin"] = (Double)frmIntrinsicPropertySearch.ntMolWeightMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntMolWeightMax.Text))
                                settings["MolWeightMax"] = (Double)frmIntrinsicPropertySearch.ntMolWeightMax.DecimalValue;

                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntAverageMassMin.Text))
                                settings["AverageMassMin"] = (Double)frmIntrinsicPropertySearch.ntAverageMassMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntAverageMassMax.Text))
                                settings["AverageMassMax"] = (Double)frmIntrinsicPropertySearch.ntAverageMassMax.DecimalValue;

                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntMonoisotopicMassMin.Text))
                                settings["MonoisotopicMassMin"] = (Double)frmIntrinsicPropertySearch.ntMonoisotopicMassMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntMonoisotopicMassMax.Text))
                                settings["MonoisotopicMassMax"] = (Double)frmIntrinsicPropertySearch.ntMonoisotopicMassMax.DecimalValue;

                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntNominalMassMin.Text))
                                settings["NominalMassMin"] = (Double)frmIntrinsicPropertySearch.ntNominalMassMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmIntrinsicPropertySearch.ntNominalMassMax.Text))
                                settings["NominalMassMax"] = (Double)frmIntrinsicPropertySearch.ntNominalMassMax.DecimalValue;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }

                case (SearchType.PredictedProperty):
                    {
                        var fields = new[] { "BoilingPointMax", "BoilingPointMin", "DensityMax", "DensityMin", "FlashPointMax", "FlashPointMin", 
                            "FreelyRotatableBondsMax", "FreelyRotatableBondsMin", "HAcceptorsMax", "HAcceptorsMin", "HDonorsMax", "HDonorsMin", 
                            "LogD55Max", "LogD55Min", "LogD74Max", "LogD74Min", "LogPMax", "LogPMin", "MolarVolumeMax", "MolarVolumeMin",
                            "PolarSurfaceAreaMax", "PolarSurfaceAreaMin", "RefractiveIndexMax", "RefractiveIndexMin","RuleOf5Max", "RuleOf5Min", 
                            "SurfaceTensionMax", "SurfaceTensionMin"};

                        foreach (var field in fields)
                        {
                            if (!settings.ContainsKey(field))
                            {
                                settings[field] = "";
                            }
                        }

                        var frmPredictedPropertySearch = new frmPredictedPropertySearch
                        {
                            ntBoilingPointMax = { Text = settings["BoilingPointMax"].ToString() },
                            ntBoilingPointMin = { Text = settings["BoilingPointMin"].ToString() },
                            ntDensityMax = { Text = settings["DensityMax"].ToString() },
                            ntDensityMin = { Text = settings["DensityMin"].ToString() },
                            ntFlashPointMax = { Text = settings["FlashPointMax"].ToString() },
                            ntFlashPointMin = { Text = settings["FlashPointMin"].ToString() },
                            ntRotatableBondsMax = { Text = settings["FreelyRotatableBondsMax"].ToString() },
                            ntRotatableBondsMin = { Text = settings["FreelyRotatableBondsMin"].ToString() },
                            ntHAcceptorsMax = { Text = settings["HAcceptorsMax"].ToString() },
                            ntHAcceptorsMin = { Text = settings["HAcceptorsMin"].ToString() },
                            ntHDonorsMax = { Text = settings["HDonorsMax"].ToString() },
                            ntHDonorsMin = { Text = settings["HDonorsMin"].ToString() },
                            ntLogD55Max = { Text = settings["LogD55Max"].ToString() },
                            ntLogD55Min = { Text = settings["LogD55Min"].ToString() },
                            ntLogD74Max = { Text = settings["LogD74Max"].ToString() },
                            ntLogD74Min = { Text = settings["LogD74Min"].ToString() },
                            ntLogPMax = { Text = settings["LogPMax"].ToString() },
                            ntLogPMin = { Text = settings["LogPMin"].ToString() },
                            ntMolarVolumeMax = { Text = settings["MolarVolumeMax"].ToString() },
                            ntMolarVolumeMin = { Text = settings["MolarVolumeMin"].ToString() },
                            ntPolarSurfaceAreaMax = { Text = settings["PolarSurfaceAreaMax"].ToString() },
                            ntPolarSurfaceAreaMin = { Text = settings["PolarSurfaceAreaMin"].ToString() },
                            ntRefractiveIndexMax = { Text = settings["RefractiveIndexMax"].ToString() },
                            ntRefractiveIndexMin = { Text = settings["RefractiveIndexMin"].ToString() },
                            ntRuleOf5Max = { Text = settings["RuleOf5Max"].ToString() },
                            ntRuleOf5Min = { Text = settings["RuleOf5Min"].ToString() },
                            ntSurfaceTensionMax = { Text = settings["SurfaceTensionMax"].ToString() },
                            ntSurfaceTensionMin = { Text = settings["SurfaceTensionMin"].ToString() }
                        };
                        if (frmPredictedPropertySearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntBoilingPointMax.Text))
                                settings["BoilingPointMax"] = (Double)frmPredictedPropertySearch.ntBoilingPointMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntBoilingPointMin.Text))
                                settings["BoilingPointMin"] = (Double)frmPredictedPropertySearch.ntBoilingPointMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntDensityMax.Text))
                                settings["DensityMax"] = (Double)frmPredictedPropertySearch.ntDensityMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntDensityMin.Text))
                                settings["DensityMin"] = (Double)frmPredictedPropertySearch.ntDensityMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntFlashPointMax.Text))
                                settings["FlashPointMax"] = (Double)frmPredictedPropertySearch.ntFlashPointMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntFlashPointMin.Text))
                                settings["FlashPointMin"] = (Double)frmPredictedPropertySearch.ntFlashPointMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRotatableBondsMax.Text))
                                settings["FreelyRotatableBondsMax"] = frmPredictedPropertySearch.ntRotatableBondsMax.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRotatableBondsMin.Text))
                                settings["FreelyRotatableBondsMin"] = frmPredictedPropertySearch.ntRotatableBondsMin.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntHAcceptorsMax.Text))
                                settings["HAcceptorsMax"] = frmPredictedPropertySearch.ntHAcceptorsMax.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntHAcceptorsMin.Text))
                                settings["HAcceptorsMin"] = frmPredictedPropertySearch.ntHAcceptorsMin.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntHDonorsMax.Text))
                                settings["HDonorsMax"] = frmPredictedPropertySearch.ntHDonorsMax.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntHDonorsMin.Text))
                                settings["HDonorsMin"] = frmPredictedPropertySearch.ntHDonorsMin.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogD55Max.Text))
                                settings["LogD55Max"] = (Double)frmPredictedPropertySearch.ntLogD55Max.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogD55Min.Text))
                                settings["LogD55Min"] = (Double)frmPredictedPropertySearch.ntLogD55Min.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogD74Max.Text))
                                settings["LogD74Max"] = (Double)frmPredictedPropertySearch.ntLogD74Max.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogD74Min.Text))
                                settings["LogD74Min"] = (Double)frmPredictedPropertySearch.ntLogD74Min.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogPMax.Text))
                                settings["LogPMax"] = (Double)frmPredictedPropertySearch.ntLogPMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntLogPMin.Text))
                                settings["LogPMin"] = (Double)frmPredictedPropertySearch.ntLogPMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntMolarVolumeMax.Text))
                                settings["MolarVolumeMax"] = (Double)frmPredictedPropertySearch.ntMolarVolumeMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntMolarVolumeMin.Text))
                                settings["MolarVolumeMin"] = (Double)frmPredictedPropertySearch.ntMolarVolumeMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntPolarSurfaceAreaMax.Text))
                                settings["PolarSurfaceAreaMax"] = (Double)frmPredictedPropertySearch.ntPolarSurfaceAreaMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntPolarSurfaceAreaMin.Text))
                                settings["PolarSurfaceAreaMin"] = (Double)frmPredictedPropertySearch.ntPolarSurfaceAreaMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRefractiveIndexMax.Text))
                                settings["RefractiveIndexMax"] = (Double)frmPredictedPropertySearch.ntRefractiveIndexMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRefractiveIndexMin.Text))
                                settings["RefractiveIndexMin"] = (Double)frmPredictedPropertySearch.ntRefractiveIndexMin.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRuleOf5Max.Text))
                                settings["RuleOf5Max"] = frmPredictedPropertySearch.ntRuleOf5Max.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntRuleOf5Min.Text))
                                settings["RuleOf5Min"] = frmPredictedPropertySearch.ntRuleOf5Min.IntValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntSurfaceTensionMax.Text))
                                settings["SurfaceTensionMax"] = (Double)frmPredictedPropertySearch.ntSurfaceTensionMax.DecimalValue;
                            if (!String.IsNullOrEmpty(frmPredictedPropertySearch.ntSurfaceTensionMin.Text))
                                settings["SurfaceTensionMin"] = (Double)frmPredictedPropertySearch.ntSurfaceTensionMin.DecimalValue;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }
            }

            return settings;
        }

        public Dictionary<string, object> SetSearchOptions(SearchType searchType, out bool cancel)
        {
            var options = new Dictionary<string, object>();
            SetCommonSearchOptions(options);
            cancel = false;

            switch (searchType)
            {
                case (SearchType.Elements):
                    {
                        var frmElementsSearch = new frmElementsSearch();
                        if (frmElementsSearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            options["IncludeAll"] = Settings.Default.IncludeAll;
                            options["IncludeElements"] = Settings.Default.IncludeElements;
                            options["ExcludeElements"] = Settings.Default.ExcludeElements;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }

                case (SearchType.Lasso):
                    {
                        var frmLASSOSearch = new frmLASSOSearch();
                        if (frmLASSOSearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            options["FamilyMax"] = Settings.Default.FamilyMax;
                            options["FamilyMin"] = Settings.Default.FamilyMin;
                            options["ThresholdMax"] = Settings.Default.ThresholdMax;
                            options["ThresholdMin"] = Settings.Default.ThresholdMin;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }

                case (SearchType.Similarity):
                    {
                        var frmSimilaritySearch = new frmSimilaritySearch();
                        if (frmSimilaritySearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            options["SimilarityThreshold"] = Settings.Default.SimilarityThreshold;
                            options["SimilarityType"] = Settings.Default.SimilarityType;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }

                case (SearchType.ExactStructure):
                    {
                        var frmExactMatchSearch = new frmExactStructureSearch();
                        if (frmExactMatchSearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            options["MatchType"] = Settings.Default.MatchType;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }

                case (SearchType.SubStructure):
                    {
                        var frmSubstructureSearch = new frmSubstructureSearch();
                        if (frmSubstructureSearch.ShowDialog(_owner) == DialogResult.OK)
                        {
                            options["MatchTautomers"] = Settings.Default.MatchTautomers;
                        }
                        else
                        {
                            cancel = true;
                        }
                        break;
                    }
            }

            return options;
        }

        public bool ExecuteSearch(string queryString, SearchType searchType, IDictionary<string, object> options)
        {
            try
            {
                _searchCancelled = false;
                _errorState = false;
                _frmProgress = new frmProgress();
                _frmProgress.FormClosed += new FormClosedEventHandler(_frmProgress_FormClosed);

                if (options == null)
                    options = new Dictionary<string, object>();

                #region set common search options

                // setup search options
                var commonSearchOptions = new CommonSearchOptions();

                // Complexity
                if (options.ContainsKey("Complexity"))
                {
                    switch (options["Complexity"].ToString())
                    {
                        case ("Any"):
                            {
                                commonSearchOptions.Complexity = EComplexity.Any;
                                break;
                            }

                        case ("Multi"):
                            {
                                commonSearchOptions.Complexity = EComplexity.Multi;
                                break;
                            }

                        case ("Single"):
                            {
                                commonSearchOptions.Complexity = EComplexity.Single;
                                break;
                            }
                        default:
                            {
                                commonSearchOptions.Complexity = EComplexity.Any;
                                break;
                            }
                    }
                }

                // Isotopic
                if (options.ContainsKey("Isotopic"))
                {
                    switch (options["Isotopic"].ToString())
                    {
                        case ("Any"):
                            {
                                commonSearchOptions.Isotopic = EIsotopic.Any;
                                break;
                            }

                        case ("Labeled"):
                            {
                                commonSearchOptions.Isotopic = EIsotopic.Labeled;
                                break;
                            }

                        case ("NotLabeled"):
                            {
                                commonSearchOptions.Isotopic = EIsotopic.NotLabeled;
                                break;
                            }
                        default:
                            {
                                commonSearchOptions.Isotopic = EIsotopic.Any;
                                break;
                            }
                    }
                }

                var hasPatents = false;
                if (options.ContainsKey("HasPatents"))
                    hasPatents = Convert.ToBoolean(options["HasPatents"]);

                commonSearchOptions.HasPatents = hasPatents;

                var hasSpectra = false;
                if (options.ContainsKey("HasSpectra"))
                    hasSpectra = Convert.ToBoolean(options["HasSpectra"]);

                commonSearchOptions.HasSpectra = hasSpectra;

                #endregion

                #region run search

                switch (searchType)
                {
                    case (SearchType.Elements):
                        {
                            var includeAll = false;
                            if (options.ContainsKey("IncludeAll"))
                                includeAll = Convert.ToBoolean(options["IncludeAll"]);

                            String[] includeElements = null;
                            if (options.ContainsKey("IncludeElements"))
                                includeElements = options["IncludeElements"].ToString().Replace(",", "|").Replace(";", "|").Replace(" ", "").Split("|".ToCharArray());

                            String[] excludeElements = null;
                            if (options.ContainsKey("ExcludeElements"))
                                excludeElements = options["ExcludeElements"].ToString().Replace(",", "|").Replace(";", "|").Replace(" ", "").Split("|".ToCharArray());

                            var searchOptions = new ElementsSearchOptions
                            {
                                IncludeAll = includeAll,
                                IncludeElements = includeElements,
                                ExcludeElements = excludeElements
                            };

                            _cs.ElementsSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.IntrinsicProperty):
                        {
                            var searchOptions = new IntrinsicPropertiesSearchOptions();

                            if (options.ContainsKey("EmpiricalFormula") && !String.IsNullOrEmpty(options["EmpiricalFormula"].ToString()))
                            {
                                searchOptions.EmpiricalFormula = options["EmpiricalFormula"].ToString();
                            }

                            if (options.ContainsKey("AverageMassMax") && !String.IsNullOrEmpty(options["AverageMassMax"].ToString()))
                            {
                                searchOptions.AverageMassMax = Convert.ToDouble(options["AverageMassMax"]);
                            }

                            if (options.ContainsKey("AverageMassMin") && !String.IsNullOrEmpty(options["AverageMassMin"].ToString()))
                            {
                                searchOptions.AverageMassMin = Convert.ToDouble(options["AverageMassMin"]);
                            }

                            if (options.ContainsKey("MolWeightMax") && !String.IsNullOrEmpty(options["MolWeightMax"].ToString()))
                            {
                                searchOptions.MolWeightMax = Convert.ToDouble(options["MolWeightMax"]);
                            }

                            if (options.ContainsKey("MolWeightMin") && !String.IsNullOrEmpty(options["MolWeightMin"].ToString()))
                            {
                                searchOptions.MolWeightMin = Convert.ToDouble(options["MolWeightMin"]);
                            }

                            if (options.ContainsKey("MonoisotopicMassMax") && !String.IsNullOrEmpty(options["MonoisotopicMassMax"].ToString()))
                            {
                                searchOptions.MonoisotopicMassMax = Convert.ToDouble(options["MonoisotopicMassMax"]);
                            }

                            if (options.ContainsKey("MonoisotopicMassMin") && !String.IsNullOrEmpty(options["MonoisotopicMassMin"].ToString()))
                            {
                                searchOptions.MonoisotopicMassMin = Convert.ToDouble(options["MonoisotopicMassMin"]);
                            }

                            if (options.ContainsKey("NominalMassMax") && !String.IsNullOrEmpty(options["NominalMassMax"].ToString()))
                            {
                                searchOptions.NominalMassMax = Convert.ToDouble(options["NominalMassMax"]);
                            }

                            if (options.ContainsKey("NominalMassMin") && !String.IsNullOrEmpty(options["NominalMassMin"].ToString()))
                            {
                                searchOptions.NominalMassMin = Convert.ToDouble(options["NominalMassMin"]);
                            }

                            _cs.IntrinsicPropertiesSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.Lasso):
                        {
                            var familyMax = new List<string>();
                            if (options.ContainsKey("FamilyMax"))
                                familyMax.AddRange(options["FamilyMax"].ToString().Replace(" ", "").Split(";".ToCharArray()));

                            var familyMin = "";
                            if (options.ContainsKey("FamilyMin"))
                                familyMin = options["FamilyMin"].ToString();

                            double thresholdMax = 0;
                            if (options.ContainsKey("ThresholdMax"))
                                thresholdMax = Convert.ToDouble(options["ThresholdMax"]);

                            double thresholdMin = 0;
                            if (options.ContainsKey("ThresholdMin"))
                                thresholdMin = Convert.ToDouble(options["ThresholdMin"]);

                            var searchOptions = new LassoSearchOptions
                            {
                                FamilyMax = familyMax.ToArray(),
                                FamilyMin = familyMin,
                                ThresholdMax = thresholdMax,
                                ThresholdMin = thresholdMin
                            };

                            _cs.LassoSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.PredictedProperty):
                        {
                            var searchOptions = new PredictedPropertiesSearchOptions();

                            if (options.ContainsKey("BoilingPointMax") && !String.IsNullOrEmpty(options["BoilingPointMax"].ToString()))
                            {
                                searchOptions.BoilingPointMax = Convert.ToDouble(options["BoilingPointMax"]);
                            }

                            if (options.ContainsKey("BoilingPointMin") && !String.IsNullOrEmpty(options["BoilingPointMin"].ToString()))
                            {
                                searchOptions.BoilingPointMin = Convert.ToDouble(options["BoilingPointMin"]);
                            }

                            if (options.ContainsKey("DensityMax") && !String.IsNullOrEmpty(options["DensityMax"].ToString()))
                            {
                                searchOptions.DensityMax = Convert.ToDouble(options["DensityMax"]);
                            }

                            if (options.ContainsKey("DensityMin") && !String.IsNullOrEmpty(options["DensityMin"].ToString()))
                            {
                                searchOptions.DensityMin = Convert.ToDouble(options["DensityMin"]);
                            }

                            if (options.ContainsKey("FlashPointMax") && !String.IsNullOrEmpty(options["FlashPointMax"].ToString()))
                            {
                                searchOptions.FlashPointMax = Convert.ToDouble(options["FlashPointMax"]);
                            }

                            if (options.ContainsKey("FlashPointMin") && !String.IsNullOrEmpty(options["FlashPointMin"].ToString()))
                            {
                                searchOptions.FlashPointMin = Convert.ToDouble(options["FlashPointMin"]);
                            }

                            if (options.ContainsKey("FreelyRotatableBondsMax") && !String.IsNullOrEmpty(options["FreelyRotatableBondsMax"].ToString()))
                            {
                                searchOptions.FreelyRotatableBondsMax = Convert.ToInt32(options["FreelyRotatableBondsMax"]);
                            }

                            if (options.ContainsKey("FreelyRotatableBondsMin") && !String.IsNullOrEmpty(options["FreelyRotatableBondsMin"].ToString()))
                            {
                                searchOptions.FreelyRotatableBondsMin = Convert.ToInt32(options["FreelyRotatableBondsMin"]);
                            }

                            if (options.ContainsKey("HAcceptorsMax") && !String.IsNullOrEmpty(options["HAcceptorsMax"].ToString()))
                            {
                                searchOptions.HAcceptorsMax = Convert.ToInt32(options["HAcceptorsMax"]);
                            }

                            if (options.ContainsKey("HAcceptorsMin") && !String.IsNullOrEmpty(options["HAcceptorsMin"].ToString()))
                            {
                                searchOptions.HAcceptorsMin = Convert.ToInt32(options["HAcceptorsMin"]);
                            }

                            if (options.ContainsKey("HDonorsMax") && !String.IsNullOrEmpty(options["HDonorsMax"].ToString()))
                            {
                                searchOptions.HDonorsMax = Convert.ToInt32(options["HDonorsMax"]);
                            }

                            if (options.ContainsKey("HDonorsMin") && !String.IsNullOrEmpty(options["HDonorsMin"].ToString()))
                            {
                                searchOptions.HDonorsMin = Convert.ToInt32(options["HDonorsMin"]);
                            }

                            if (options.ContainsKey("LogD55Max") && !String.IsNullOrEmpty(options["LogD55Max"].ToString()))
                            {
                                searchOptions.LogD55Max = Convert.ToDouble(options["LogD55Max"]);
                            }

                            if (options.ContainsKey("LogD55Min") && !String.IsNullOrEmpty(options["LogD55Min"].ToString()))
                            {
                                searchOptions.LogD55Min = Convert.ToDouble(options["LogD55Min"]);
                            }

                            if (options.ContainsKey("LogD74Max") && !String.IsNullOrEmpty(options["LogD74Max"].ToString()))
                            {
                                searchOptions.LogD74Max = Convert.ToDouble(options["LogD74Max"]);
                            }

                            if (options.ContainsKey("LogD74Min") && !String.IsNullOrEmpty(options["LogD74Min"].ToString()))
                            {
                                searchOptions.LogD74Min = Convert.ToDouble(options["LogD74Min"]);
                            }

                            if (options.ContainsKey("LogPMax") && !String.IsNullOrEmpty(options["LogPMax"].ToString()))
                            {
                                searchOptions.LogPMax = Convert.ToDouble(options["LogPMax"]);
                            }

                            if (options.ContainsKey("LogPMin") && !String.IsNullOrEmpty(options["LogPMin"].ToString()))
                            {
                                searchOptions.LogPMin = Convert.ToDouble(options["LogPMin"]);
                            }

                            if (options.ContainsKey("MolarVolumeMax") && !String.IsNullOrEmpty(options["MolarVolumeMax"].ToString()))
                            {
                                searchOptions.MolarVolumeMax = Convert.ToDouble(options["MolarVolumeMax"]);
                            }

                            if (options.ContainsKey("MolarVolumeMin") && !String.IsNullOrEmpty(options["MolarVolumeMin"].ToString()))
                            {
                                searchOptions.MolarVolumeMin = Convert.ToDouble(options["MolarVolumeMin"]);
                            }

                            if (options.ContainsKey("PolarSurfaceAreaMax") && !String.IsNullOrEmpty(options["PolarSurfaceAreaMax"].ToString()))
                            {
                                searchOptions.PolarSurfaceAreaMax = Convert.ToDouble(options["PolarSurfaceAreaMax"]);
                            }

                            if (options.ContainsKey("PolarSurfaceAreaMin") && !String.IsNullOrEmpty(options["PolarSurfaceAreaMin"].ToString()))
                            {
                                searchOptions.PolarSurfaceAreaMin = Convert.ToDouble(options["PolarSurfaceAreaMin"]);
                            }

                            if (options.ContainsKey("RefractiveIndexMax") && !String.IsNullOrEmpty(options["RefractiveIndexMax"].ToString()))
                            {
                                searchOptions.RefractiveIndexMax = Convert.ToDouble(options["RefractiveIndexMax"]);
                            }

                            if (options.ContainsKey("RefractiveIndexMin") && !String.IsNullOrEmpty(options["RefractiveIndexMin"].ToString()))
                            {
                                searchOptions.RefractiveIndexMin = Convert.ToDouble(options["RefractiveIndexMin"]);
                            }

                            if (options.ContainsKey("RuleOf5Max") && !String.IsNullOrEmpty(options["RuleOf5Max"].ToString()))
                            {
                                searchOptions.RuleOf5Max = Convert.ToInt32(options["RuleOf5Max"]);
                            }

                            if (options.ContainsKey("RuleOf5Min") && !String.IsNullOrEmpty(options["RuleOf5Min"].ToString()))
                            {
                                searchOptions.RuleOf5Min = Convert.ToInt32(options["RuleOf5Min"]);
                            }

                            if (options.ContainsKey("SurfaceTensionMax") && !String.IsNullOrEmpty(options["SurfaceTensionMax"].ToString()))
                            {
                                searchOptions.SurfaceTensionMax = Convert.ToDouble(options["SurfaceTensionMax"]);
                            }

                            if (options.ContainsKey("SurfaceTensionMin") && !String.IsNullOrEmpty(options["SurfaceTensionMin"].ToString()))
                            {
                                searchOptions.SurfaceTensionMin = Convert.ToDouble(options["SurfaceTensionMin"]);
                            }

                            _cs.PredictedPropertiesSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.Similarity):
                        {
                            float threshold = 0;
                            if (options.ContainsKey("SimilarityThreshold"))
                                threshold = Convert.ToSingle(options["SimilarityThreshold"]);

                            var searchOptions = new SimilaritySearchOptions
                            {
                                Molecule = queryString,
                                Threshold = threshold
                            };

                            // SimilarityType
                            if (options.ContainsKey("SimilarityType"))
                            {
                                switch (options["SimilarityType"].ToString())
                                {
                                    case ("Euclidian"):
                                        {
                                            searchOptions.SimilarityType = ESimilarityType.Euclidian;
                                            break;
                                        }

                                    case ("Tanimoto"):
                                        {
                                            searchOptions.SimilarityType = ESimilarityType.Tanimoto;
                                            break;
                                        }

                                    case ("Tversky"):
                                        {
                                            searchOptions.SimilarityType = ESimilarityType.Tversky;
                                            break;
                                        }

                                    default:
                                        {
                                            searchOptions.SimilarityType = ESimilarityType.Tanimoto;
                                            break;
                                        }
                                }
                            }

                            _cs.SimilaritySearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.Simple):
                        {
                            if (string.IsNullOrEmpty(queryString))
                            {
                                var frmSimpleSearch = new frmSimpleSearch();
                                if (frmSimpleSearch.ShowDialog(_owner) == DialogResult.OK)
                                {
                                    _cs.SimpleSearchAsync(frmSimpleSearch.txtQuery.Text, _token);
                                    _frmProgress.ShowDialog(_owner);
                                }
                                else
                                {
                                    _owner.Cursor = Cursors.Default;
                                }
                            }
                            else
                            {
                               _cs.SimpleSearchAsync(queryString, _token);
                               _frmProgress.ShowDialog(_owner);
                            }                         
                            break;
                        }

                    case (SearchType.ExactStructure):
                        {
                            var searchOptions = new ExactStructureSearchOptions
                            {
                                Molecule = queryString,
                            };

                            // MatchType
                            if (options.ContainsKey("MatchType"))
                            {
                                switch (options["MatchType"].ToString())
                                {
                                    case ("ExactMatch"):
                                        {
                                            searchOptions.MatchType = EMatchType.ExactMatch;
                                            break;
                                        }

                                    case ("AllIsomers"):
                                        {
                                            searchOptions.MatchType = EMatchType.AllIsomers;
                                            break;
                                        }

                                    case ("AllTautomers"):
                                        {
                                            searchOptions.MatchType = EMatchType.AllTautomers;
                                            break;
                                        }

                                    case ("SameSkeletonExcludingH"):
                                        {
                                            searchOptions.MatchType = EMatchType.SameSkeletonExcludingH;
                                            break;
                                        }

                                    case ("SameSkeletonIncludingH"):
                                        {
                                            searchOptions.MatchType = EMatchType.SameSkeletonIncludingH;
                                            break;
                                        }
                                    default:
                                        {
                                            searchOptions.MatchType = EMatchType.ExactMatch;
                                            break;
                                        }
                                }
                            }

                            _cs.StructureSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                    case (SearchType.SubStructure):
                        {
                            var matchTautomers = false;
                            if (options.ContainsKey("MatchTautomers"))
                                matchTautomers = Convert.ToBoolean(options["MatchTautomers"]);

                            var searchOptions = new SubstructureSearchOptions
                            {
                                Molecule = queryString,
                                MatchTautomers = matchTautomers
                            };

                            _cs.SubstructureSearchAsync(searchOptions, commonSearchOptions, _token);
                            _frmProgress.ShowDialog(_owner);
                            break;
                        }

                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(_owner, "Unexpected error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        void _frmProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_frmProgress.DialogResult == DialogResult.Cancel)
            {
                _searchCancelled = true;
                _owner.Cursor = Cursors.Default;
                if (_bgwGetResults != null)
                {
                    _bgwGetResults.CancelAsync();
                }
            }
        }

        #endregion

        #region Search Completed Events

        // ReSharper disable InconsistentNaming
        private void _cs_GetAsyncSearchResultPartCompleted(object sender, GetAsyncSearchResultPartCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (e.Error == null && !_searchCancelled)
            {
                Results = e.Result;
                if (Results != null && Results.Length > 0)
                {
                    var resultsForm = new frmResults(Results, _cs, _ci, _token, _transferType, _closeOnTransfer)
                    {
                        Icon = _owner.Icon,
                        Text = Application.ProductName + ": ChemSpider " + _searchType + " Search Results"
                    };
                    resultsForm.StructureTransfering += (resultsForm_StructureTransfering);
                    _frmProgress.Close();
                    resultsForm.Show(_owner);
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    Application.DoEvents();
                    if (!_errorState)
                        MessageBox.Show(_owner, "Search found no hits", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            else
            {
                _errorState = true;
                _frmProgress.Close();
                Application.DoEvents();
                _owner.Cursor = Cursors.Default;
                MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        // ReSharper disable InconsistentNaming
        void _cs_GetAsyncSearchResultCompleted(object sender, GetAsyncSearchResultCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (e.Error == null && !_searchCancelled)
            {
                Results = e.Result;
                if (Results != null && Results.Length > 0)
                {
                    var resultsForm = new frmResults(Results, _cs, _ci, _token, _transferType, _closeOnTransfer)
                    {
                        Icon = _owner.Icon,
                        Text = Application.ProductName + ": ChemSpider " + _searchType + " Search Results"
                    };
                    resultsForm.StructureTransfering += (resultsForm_StructureTransfering);
                    _frmProgress.Close();
                    resultsForm.Show(_owner);
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    Application.DoEvents();
                    if (!_errorState)
                    {
                        MessageBox.Show(_owner, "Search found no hits", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                _errorState = true;
                _frmProgress.Close();
                Application.DoEvents();
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_PredictedPropertiesSearchCompleted(object sender, PredictedPropertiesSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Predicted Properties");
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_IntrinsicPropertiesSearchCompleted(object sender, IntrinsicPropertiesSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Intrinsic Properties");

                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_LassoSearchCompleted(object sender, LassoSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "LASSO");

                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_ElementsSearchCompleted(object sender, ElementsSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Elements");

                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_SimilaritySearchCompleted(object sender, SimilaritySearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Similarity");
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_SimpleSearchCompleted(object sender, SimpleSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    if (e.Result.Length != 0)
                    {
                        Results = e.Result;
                        var resultsForm = new frmResults(Results, _cs, _ci, _token, _transferType, _closeOnTransfer) 
                        { Icon = _owner.Icon, Text = Application.ProductName + ": ChemSpider Text Search Results" };
                        resultsForm.StructureTransfering += (resultsForm_StructureTransfering);
                        _frmProgress.Close();
                        Application.DoEvents();
                        resultsForm.Show(_owner);
                    }
                    else
                    {
                        _owner.Cursor = Cursors.Default;
                        _frmProgress.Close();
                        Application.DoEvents();
                        if (!_errorState)
                            MessageBox.Show(_owner, "Search found no hits", Application.ProductName, MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }

                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    Application.DoEvents();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                if (!_errorState)
                    MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_StructureSearchCompleted(object sender, StructureSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Exact Structure");
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                 MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // ReSharper disable InconsistentNaming
        void cs_SubstructureSearchCompleted(object sender, SubstructureSearchCompletedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!e.Cancelled && !_searchCancelled)
            {
                if (e.Error == null)
                {
                    GetResults(e.Result, "Substructure");
                }
                else
                {
                    _owner.Cursor = Cursors.Default;
                    _frmProgress.Close();
                    MessageBox.Show(_owner, "Error executing search: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);    
                }

            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                MessageBox.Show(_owner, "Search cancelled by user", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void GetResults(string result, string searchType)
        {
            _bgwGetResults = new BackgroundWorker();
            _bgwGetResults.WorkerSupportsCancellation = true;
            _bgwGetResults.DoWork += new DoWorkEventHandler(_bgwGetResults_DoWork);
            _bgwGetResults.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgwGetResults_RunWorkerCompleted);
            var resultArgs = new object[] { result, searchType };
            if (!String.IsNullOrEmpty(result))
            {
                if (!_searchCancelled)
                    _bgwGetResults.RunWorkerAsync(resultArgs);
            }
            else
            {
                _owner.Cursor = Cursors.Default;
                _frmProgress.Close();
                Application.DoEvents();
                if (!_errorState)
                    MessageBox.Show(_owner, "Search found no hits", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        void _bgwGetResults_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;
            var result = args[0].ToString();
            var searchType = args[1].ToString();


            _searchType = searchType;
            var searchStatus = _cs.GetAsyncSearchStatus(result, _token);

            if (searchStatus == ERequestStatus.Processing | searchStatus == ERequestStatus.Scheduled)
            {

                //var count = 0;
                while (searchStatus == ERequestStatus.Processing | searchStatus == ERequestStatus.Scheduled)
                {
                    Thread.Sleep(1000);
                    searchStatus = _cs.GetAsyncSearchStatus(result, _token);
                    if (_searchCancelled)
                        break;
                    //count++;
                }
            }

            var resultArgs = new object[] { result };
            e.Result = resultArgs;
        }

        void _bgwGetResults_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var args = (object[])e.Result;
            var result = args[0].ToString();
            if (!_searchCancelled && !e.Cancelled)
            {
                var searchStatus = _cs.GetAsyncSearchStatus(result, _token);

                if (searchStatus == ERequestStatus.ResultReady)
                {
                    _cs.GetAsyncSearchResultAsync(result, _token);
                }
                else if (searchStatus == ERequestStatus.PartialResultReady)
                {
                    _cs.GetAsyncSearchResultPartAsync(result, _token, 0, 0);
                }
                else
                {
                    _frmProgress.Close();
                    _owner.Cursor = Cursors.Default;
                    MessageBox.Show(_owner, "Search failed (search status: " + searchStatus + ")", Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                }
            }
        }

        // ReSharper disable InconsistentNaming
        void resultsForm_StructureTransfering(object sender, StructureTransferEventsArgs e)
        // ReSharper restore InconsistentNaming
        {
            StructureTransfered(this, e);
        }

        #endregion

    }

    public class Register
    {

    }


    public class StructureTransferEventsArgs : EventArgs
    {
        private String _smiles;
        private String _inchi;
        private String _inchiKey;
        private String _molfileString;
        private int _csid;
        private Image _thumbNail;

        public String SMILES
        {
            set
            {
                _smiles = value;
            }
            get
            {
                return _smiles;
            }
        }

        public String MolfileString
        {
            set
            {
                _molfileString = value;
            }
            get
            {
                return _molfileString;
            }
        }

        public int CSID
        {
            set
            {
                _csid = value;
            }
            get
            {
                return _csid;
            }
        }

        public string InChI
        {
            set
            {
                _inchi = value;
            }
            get
            {
                return _inchi;
            }
        }

        public string InChIKey
        {
            set
            {
                _inchiKey = value;
            }
            get
            {
                return _inchiKey;
            }
        }

        public Image ThumbNail
        {
            set
            {
                _thumbNail = value;
            }
            get
            {
                return _thumbNail;
            }
        }
    }

}
