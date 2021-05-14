using eLibraryPortal.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Core.Helper
{
    public class Utility
    {
        public static string ShowMessage(AlertMessage webalert = null)
        {
            if (webalert != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("$(document).ready(function () {");
                sb.Append("swal({");
                sb.Append("title:'" + webalert.Title + "',");
                sb.Append("text:'" + webalert.Message + "',");

                if (webalert.MessageType == MessageType.InfoMessage)
                {
                    sb.Append("icon:'info'");
                }
                else if (webalert.MessageType == MessageType.SuccessMessage)
                {
                    sb.Append("icon:'success'");
                }
                else if (webalert.MessageType == MessageType.ErrorMessage)
                {
                    sb.Append("icon:'error'");
                }
                else if (webalert.MessageType == MessageType.Warning)
                {
                    sb.Append("icon:'warning'");
                }

                sb.Append("})})");

                return sb.ToString();

            }
            else
            {

                return string.Empty;
            }

        }

       

    }
}
