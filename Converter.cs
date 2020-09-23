using System;

namespace easyconverter
{
    public static class DataConverter
    {
        public static string[] ConvertValues(string inputValue, short conv_type)
        {
            if(inputValue=="хуйня")
            {
                MainWindow.convert_status = -1;
                return null;
            }
            string[] result = new string[4];
            int value;
            try
            {
               switch(conv_type)
               {
                    case 0:
                        value = Convert.ToInt32(inputValue,16);
                        break;
                    case 1:
                        value = Int32.Parse(inputValue);
                        break;
                    case 2:
                        value = Convert.ToInt32(inputValue,2);
                        break;
                    case 3:
                        if(inputValue[1]=='o'){
                            inputValue = inputValue.Replace('o','0');
                            Console.WriteLine(inputValue);
                        }
                        value = Convert.ToInt32(inputValue,8);
                        break;
                    default:
                        MainWindow.convert_status = -3;
                        return null;
               }
            }
            catch
            {
                MainWindow.convert_status = -3;
                return null;
            }

            if(value>9999999 || value<0)
            {
                MainWindow.convert_status = -2;
                return null;
            }

            result[0] = $"0x{value:X}";
            result[1] = value.ToString();
            result[2] = Convert.ToString(value,2);
            result[3] = $"0o{Convert.ToString(value,8)}";


            MainWindow.convert_status = 0;
            return result;
        }

    }
}