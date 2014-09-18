namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;

    public class Field
    {
        internal Color color_0;
        internal Color color_1;
        public string FieldName;
        public string HeaderName;
        public bool isTotalField;
        internal string string_0;
        internal string string_1;
        internal string string_2;
        public int Width;

        public Field()
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = "";
            this.HeaderName = "Column Header";
            this.BackColor = Color.White;
            this.Width = 0;
            this.HeaderBackColor = Color.White;
        }

        public Field(string fieldName, string headerName)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.BackColor = Color.White;
            this.Width = 0;
            this.HeaderBackColor = Color.White;
        }

        public Field(string fieldName, string headerName, Color headerBgColor)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.Width = 0;
            this.BackColor = Color.White;
            this.HeaderBackColor = headerBgColor;
        }

        public Field(string fieldName, string headerName, int width)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.BackColor = Color.White;
            this.Width = width;
            this.HeaderBackColor = Color.White;
        }

        public Field(string fieldName, string headerName, Color bgcolor, Color headerBgColor)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.Width = 0;
            this.BackColor = bgcolor;
            this.HeaderBackColor = headerBgColor;
        }

        public Field(string fieldName, string headerName, int width, ALIGN TextAlignment)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.BackColor = Color.White;
            this.Width = width;
            this.BackColor = Color.White;
            this.HeaderBackColor = Color.White;
            this.Alignment = TextAlignment;
        }

        public Field(string fieldName, string headerName, int width, Color bgcolor)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.BackColor = Color.White;
            this.Width = width;
            this.BackColor = bgcolor;
            this.HeaderBackColor = Color.White;
        }

        public Field(string fieldName, string headerName, int width, Color bgcolor, Color headerBgColor)
        {
            this.isTotalField = false;
            this.string_2 = "LEFT";
            this.FieldName = fieldName;
            this.HeaderName = headerName;
            this.Width = width;
            this.BackColor = bgcolor;
            this.HeaderBackColor = headerBgColor;
        }

        public ALIGN Alignment
        {
            get
            {
                string str = this.string_2;
                switch (str)
                {
                    case null:
                        break;

                    case "LEFT":
                        return ALIGN.LEFT;

                    default:
                        if (!(str == "RIGHT"))
                        {
                            if (str == "CENTER")
                            {
                                return ALIGN.CENTER;
                            }
                        }
                        else
                        {
                            return ALIGN.RIGHT;
                        }
                        break;
                }
                return ALIGN.LEFT;
            }
            set
            {
                switch (value)
                {
                    case ALIGN.LEFT:
                        this.string_2 = "LEFT";
                        break;

                    case ALIGN.RIGHT:
                        this.string_2 = "RIGHT";
                        break;

                    case ALIGN.CENTER:
                        this.string_2 = "CENTER";
                        break;

                    default:
                        this.string_2 = "LEFT";
                        break;
                }
            }
        }

        public Color BackColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.string_0 = Class4.smethod_0(value);
                this.color_0 = value;
            }
        }

        public Color HeaderBackColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.string_1 = Class4.smethod_0(value);
                this.color_1 = value;
            }
        }
    }
}

