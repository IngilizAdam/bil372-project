using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _372_project
{
    class Constants
    {
        public static Dictionary<string, string> ATTR_TO_NAME_DICT = new Dictionary<string, string>()
        {
            {"stud_id",         "Öğrenci ID"},
            {"stud_fname",      "Öğrenci Adı"},
            {"stud_lname",      "Öğrenci Soyadı"},
            {"stud_birth_year", "Öğrenci Doğum Tarihi"},
            {"stud_sex",        "Öğrenci Cinsiyeti"},
            {"stud_reg_date",   "Öğrenci Kayıt Tarihi"},
            {"stud_gpa",        "Öğrenci GPA"},
            {"stud_email",      "Öğrenci E-Mail"},
            {"stud_number",     "Öğrenci Telefon Numarası"},

            {"empl_id"           , "Çalışan ID" },
            {"empl_type"         , "Çalışan Tipi"},
            {"empl_fname"        , "Çalışan İsmi"},
            {"empl_lname"        , "Çalışan Soyismi"},
            {"empl_salary"       , "Çalışan Maaşı"},
            {"empl_reg_date"     , "Çalışan Başlama Tarihi"},

            {"grad_grad_date"    , "Mezun Mezun Olma Tarihi"},

            {"pare_id"           , "Veli ID"},
            {"pare_fname"        , "Veli İsmi"},
            {"pare_lname"        , "Veli Soyismi"},
            {"pare_sex"          , "Veli Cinsiyeti"},
            {"pare_email"        , "Veli E-Mail"},
            {"pare_number"       , "Veli Telefon Numarası"},

            {"exps_id"           , "Harcama ID"},
            {"exps_name"         , "Harcama İsmi"},
            {"exps_date"         , "Harcama Tarihi"},
            {"exps_cost"         , "Harcama Tutarı"},
            {"exps_type"         , "Harcama Tipi"},

            {"stck_id"           , "Stok ID"},
            {"stck_name"         , "Stok İsmi"},
            {"stck_count"        , "Stok Ürün Sayısı"},

            {"cour_id"           , "Ders ID"},
            {"cour_min_req"      , "Ders Minimum Talep Sayısı"},
            {"cour_instructor"   , "Ders Eğitmen ID'si"},

            {"lect_id"           , "Lecture ID"},
            {"lect_hour"         , "Lecture Saati"},
            {"lect_day"          , "Lecture Günü"},

            {"avail_hour"        , "Uygun Olunan Saat"},
            {"avail_day"         , "Uygun Olunan Gün"},

            {"rela_type"         , "Akraba Yakınlık İlişkisi"},

            {"need_amount"       , "İhtiyaç Duyulan Miktar"},
        };

        public static Dictionary<string, string> NAME_TO_ATTR_DICT = new Dictionary<string, string>()
        {
            {"Öğrenci ID",               "stud_id"},
            {"Öğrenci Adı",              "stud_fname"},
            {"Öğrenci Soyadı",           "stud_lname"},
            {"Öğrenci Doğum Tarihi",     "stud_birth_year"},
            {"Öğrenci Cinsiyeti",        "stud_sex"},
            {"Öğrenci Kayıt Tarihi",     "stud_reg_date"},
            {"Öğrenci GPA",              "stud_gpa"},
            {"Öğrenci E-Mail",           "stud_email"},
            {"Öğrenci Telefon Numarası", "stud_number" },

            {"Çalışan ID"           , "empl_id"         },
            {"Çalışan Tipi"         , "empl_type"       },
            {"Çalışan İsmi"         , "empl_fname"      },
            {"Çalışan Soyismi"      , "empl_lname"      },
            {"Çalışan Maaşı"        , "empl_salary"     },
            {"Çalışan Başlama Tarihi", "empl_reg_date"   },

            {"Mezun Mezun Olma Tarihi", "grad_grad_date"  },

            {"Veli ID"              , "pare_id"         },
            {"Veli İsmi"            , "pare_fname"      },
            {"Veli Soyismi"          , "pare_lname"      },
            {"Veli Cinsiyeti"       , "pare_sex"        },
            {"Veli E-Mail"          , "pare_email"      },
            {"Veli Telefon Numarası" , "pare_number"     },

            {"Harcama ID"          , "exps_id"         },
            {"Harcama İsmi"        , "exps_name"       },
            {"Harcama Tarihi"      , "exps_date"       },
            {"Harcama Tutarı"      , "exps_cost"       },
            {"Harcama Tipi"        , "exps_type"       },

            {"Stok ID"             , "stck_id"         },
            {"Stok İsmi"           , "stck_name"       },
            {"Stok Ürün Sayısı"    , "stck_count"      },

            {"Ders ID"             , "cour_id"         },
            {"Ders Minimum Talep Sayısı", "cour_min_req"},
            {"Ders Eğitmen ID'si"   , "cour_instructor" },

            {"Lecture ID"          , "lect_id"         },
            {"Lecture Saati"       , "lect_hour"       },
            {"Lecture Günü"        , "lect_day"        },

            {"Uygun Olunan Saat"   , "avail_hour"      },
            {"Uygun Olunan Gün"    , "avail_day"       },

            {"Akraba Yakınlık İlişkisi", "rela_type"  },

            {"İhtiyaç Duyulan Miktar", "need_amount"   },
        };
    }
}
