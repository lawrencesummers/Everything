namespace WHC.OrderWater.Commons
{
    using System;
    using System.Data;
    using System.Data.SqlTypes;

    public sealed class SmartDataReader
    {
        private DateTime dateTime_0 = Convert.ToDateTime("01/01/1900 00:00:00");
        private IDataReader idataReader_0;

        public SmartDataReader(IDataReader reader)
        {
            this.idataReader_0 = reader;
        }

        public T? ConvertToNullableValue<T>(object value) where T: struct
        {
            if (value == null)
            {
                return null;
            }
            if (value == DBNull.Value)
            {
                return null;
            }
            if ((value is string) && string.IsNullOrEmpty((string) value))
            {
                return null;
            }
            if (!(value is T))
            {
                try
                {
                    value = Convert.ChangeType(value, typeof(T));
                }
                catch (Exception exception)
                {
                    throw new ArgumentException("Value值不是一个有效的类型", "value", exception);
                }
            }
            return new T?((T) value);
        }

        public bool GetBoolean(string column)
        {
            return this.GetBoolean(column, false);
        }

        public bool GetBoolean(string column, bool defaultIfNull)
        {
            bool flag;
            string str = this.idataReader_0[column].ToString();
            try
            {
                flag = Convert.ToInt32(str) > 0;
            }
            catch
            {
            }
            return flag;
        }

        public bool? GetBooleanNullable(string column)
        {
            bool? nullable2;
            string str = this.idataReader_0[column].ToString();
            try
            {
                nullable2 = new bool?(Convert.ToInt32(str) > 0);
            }
            catch
            {
            }
            return nullable2;
        }

        public byte[] GetBytes(string column)
        {
            return this.GetBytes(column, null);
        }

        public byte[] GetBytes(string column, string defaultIfNull)
        {
            int ordinal = this.idataReader_0.GetOrdinal(column);
            if (!this.idataReader_0.IsDBNull(ordinal))
            {
                long num5 = this.idataReader_0.GetBytes(ordinal, 0, null, 0, 0);
                byte[] buffer = new byte[num5];
                int length = 0x400;
                long num2 = 0;
                for (int i = 0; num2 < num5; i += length)
                {
                    num2 += this.idataReader_0.GetBytes(ordinal, (long) i, buffer, i, length);
                }
                return buffer;
            }
            return null;
        }

        public DateTime GetDateTime(string column)
        {
            return this.GetDateTime(column, this.dateTime_0);
        }

        public DateTime GetDateTime(string column, DateTime defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : Convert.ToDateTime(this.idataReader_0[column].ToString()));
        }

        public DateTime? GetDateTimeNullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new DateTime?(Convert.ToDateTime(this.idataReader_0[column].ToString())));
        }

        public decimal GetDecimal(string column)
        {
            return this.GetDecimal(column, 0M);
        }

        public decimal GetDecimal(string column, decimal defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : decimal.Parse(this.idataReader_0[column].ToString()));
        }

        public decimal? GetDecimalNullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new decimal?(decimal.Parse(this.idataReader_0[column].ToString())));
        }

        public double GetDouble(string column)
        {
            return this.GetDouble(column, 0.0);
        }

        public double GetDouble(string column, double defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : double.Parse(this.idataReader_0[column].ToString()));
        }

        public double? GetDoubleNullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new double?(double.Parse(this.idataReader_0[column].ToString())));
        }

        public float GetFloat(string column)
        {
            return this.GetFloat(column, 0f);
        }

        public float GetFloat(string column, float defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : float.Parse(this.idataReader_0[column].ToString()));
        }

        public float? GetFloatNullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new float?(float.Parse(this.idataReader_0[column].ToString())));
        }

        public Guid GetGuid(string column)
        {
            return this.GetGuid(column, null);
        }

        public Guid GetGuid(string column, string defaultIfNull)
        {
            string g = this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : this.idataReader_0[column].ToString();
            Guid empty = Guid.Empty;
            if (g != null)
            {
                empty = new Guid(g);
            }
            return empty;
        }

        public Guid? GetGuidNullable(string column)
        {
            string g = this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : this.idataReader_0[column].ToString();
            Guid? nullable = null;
            if (g != null)
            {
                nullable = new Guid(g);
            }
            return nullable;
        }

        public short GetInt16(string column)
        {
            return this.GetInt16(column, 0);
        }

        public short GetInt16(string column, short defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : short.Parse(this.idataReader_0[column].ToString()));
        }

        public short? GetInt16Nullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new short?(short.Parse(this.idataReader_0[column].ToString())));
        }

        public int GetInt32(string column)
        {
            return this.GetInt32(column, 0);
        }

        public int GetInt32(string column, int defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : int.Parse(this.idataReader_0[column].ToString()));
        }

        public int? GetInt32Nullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new int?(int.Parse(this.idataReader_0[column].ToString())));
        }

        public float GetSingle(string column)
        {
            return this.GetSingle(column, 0f);
        }

        public float GetSingle(string column, float defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : float.Parse(this.idataReader_0[column].ToString()));
        }

        public float? GetSingleNullable(string column)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? null : new float?(float.Parse(this.idataReader_0[column].ToString())));
        }

        public string GetString(string column)
        {
            return this.GetString(column, "");
        }

        public string GetString(string column, string defaultIfNull)
        {
            return (this.idataReader_0.IsDBNull(this.idataReader_0.GetOrdinal(column)) ? defaultIfNull : this.idataReader_0[column].ToString());
        }

        public bool IsNull(object value)
        {
            return ((value == null) || (((value is INullable) && ((INullable) value).IsNull) || (value == DBNull.Value)));
        }

        public bool Read()
        {
            return this.idataReader_0.Read();
        }
    }
}

