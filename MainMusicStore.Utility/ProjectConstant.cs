using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.Utility
{
    public static class ProjectConstant
    {
        public const string ResultNotFound = "Data Not Found ";
        //gibi değişkenler tutulacaktır sabitler birden çok yerde kullanılan.
        //---------------------------------------------------------------------//
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";
        //---------------------------------------------------------------------//
    }
}
