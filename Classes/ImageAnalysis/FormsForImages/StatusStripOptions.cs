using System.Windows.Forms;
using System.Windows.Forms.Design;
using System;
using HCSAnalyzer.Forms.FormsForImages;


[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                                       ToolStripItemDesignerAvailability.ContextMenuStrip |
                                       ToolStripItemDesignerAvailability.StatusStrip)]

public class NumericUpDownStripItem : ToolStripControlHost
{
    public NumericUpDown ThisNumericUpDown;
    FormForImageDisplay Parent;


    public NumericUpDownStripItem(FormForImageDisplay Parent)
        : base(new NumericUpDown())
    {
        this.ThisNumericUpDown = this.Control as NumericUpDown;

        this.ThisNumericUpDown.Maximum = 2000;
        this.Size = new System.Drawing.Size(83, 20);
        this.Parent = Parent;

        this.ThisNumericUpDown.ValueChanged += new EventHandler(ThisNumericUpDown_ValueChanged);
    }

    void ThisNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        Parent.ChangeZoomValue((int)this.ThisNumericUpDown.Value);

    }


}