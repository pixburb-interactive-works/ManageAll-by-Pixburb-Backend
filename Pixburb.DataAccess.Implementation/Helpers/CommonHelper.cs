using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Implementation.Helpers
{
    internal static class CommonHelper
    {
        public static string CheckDefaultBooleanValue(bool value)
        {
            if (value)
            {
                return "Y";
            }

            return "N";
        }

        public static string CheckDefaultNullableBooleanValue(bool? value)
        {
            if (value == null)
            {
                return null;
            }
            else if (value == true)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        public static string CheckDefaultBooleanValues(bool value)
        {
            if (value)
            {
                return "Y";
            }

            return null;
        }

        public static bool GetBooleanFromString(string value)
        {
            if (value == "Y")
            {
                return true;
            }

            return false;
        }

        public static bool CheckDefaultStringToBooleanValue(char value)
        {
            if (value == 'Y')
            {
                return true;
            }

            return false;
        }

        public static object CheckDefaultDate(DateTime date)
        {
            if (date == default(DateTime))
            {
                return DBNull.Value;
            }
            else
            {
                return date;
            }
        }

        public static object CheckNullValue(int? id)
        {
            if (id == default(long))
            {
                return DBNull.Value;
            }
            else
            {
                return id;
            }
        }

        public static object GetRecordStatus(string status)
        {
            if (status == "I")
            {
                return DBNull.Value;
            }
            else
            {
                return status;
            }
        }

        internal static object RemoveNullableDefaultValue(long? value)
        {
            if (value == null || value == 0)
            {
                return DBNull.Value;
            }

            return value;
        }

        internal static object GetBooleanValue(this bool value)
        {
            if (value)
            {
                return 'Y';
            }

            return 'N';
        }

        internal static object GetRecordStatusById(this long value)
        {
            if (value == default(long))
            {
                return 'I';
            }

            return 'U';
        }

        internal static object GetRecordStatusByNullId(this long? value)
        {
            if (value == default(long) || value == null)
            {
                return 'I';
            }

            return 'U';
        }

        internal static object RemoveDefaultValue1(long? value)
        {
            if (value == null || value == 0)
            {
                return DBNull.Value;
            }

            return value;
        }

        internal static object RemoveDefaultValue(string value)
        {
            if (value == null || value == string.Empty)
            {
                return DBNull.Value;
            }

            return value;
        }

        internal static decimal? RemoveZeroForDecimal(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(value);
        }

        internal static object ToConvertDateFormat(long? value)
        {
            if (value == null || value == 0)
            {
                return DBNull.Value;
            }

            return value;
        }

        internal static object RemoveDefaultNullableDecimal(decimal? value)
        {
            if (value == null || value == 0)
            {
                return DBNull.Value;
            }

            return value;
        }

        internal static object RemoveDefaultValue<T>(T value)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    if (string.IsNullOrWhiteSpace(value as string))
                    {
                        return DBNull.Value;
                    }

                    return value;

                case TypeCode.DateTime:
                    DateTime converted = Convert.ToDateTime(value);
                    DateTime thisDate;
                    string dateFormat;
                    if (converted.Date.ToString() == default(DateTime).ToString())
                    {
                        return DBNull.Value;
                    }
                    else if (converted.Date.Kind != DateTimeKind.Utc)
                    {
                        thisDate = converted.Date;
                        dateFormat = thisDate.ToString("dd/MMM/yyyy");
                        return dateFormat;
                    }
                    else
                    {
                        thisDate = converted.Date;
                        return thisDate;
                    }

                default:
                    if (EqualityComparer<T>.Default.Equals(value, default(T)))
                    {
                        return DBNull.Value;
                    }

                    return value;
            }
        }
    }
}
