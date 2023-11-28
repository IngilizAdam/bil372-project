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
            {"stud_id"           , "Öğrenci ID"},
            {"stud_fname"        , "Öğrenci Adı"},
            {"stud_lname"        , "Öğrenci Soyadı"},
            {"stud_birth_year"   , "Öğrenci Doğum Tarihi"},
            {"stud_sex"          , "Öğrenci Cinsiyeti"},
            {"stud_reg_date"     , "Öğrenci Kayıt Tarihi"},
            {"stud_gpa"          , "Öğrenci GPA"},
            {"stud_email"        , "Öğrenci E-Mail"},
            {"stud_number"       , "Öğrenci Telefon Numarası"},

            {"empl_id"           , "Çalışan ID" },
            {"empl_type"         , "Çalışan Tipi"},
            {"empl_fname"        , "Çalışan İsmi"},
            {"empl_lname"        , "Çalışan Soyismi"},
            {"empl_salary"       , "Çalışan Maaşı"},
            {"empl_reg_date"     , "Çalışan Başlama Tarihi"},

            {"grad_grad_date"    , "Mezun Mezun Olma Tarihi"},

            {"pare_id"           , "Yakın ID"},
            {"pare_fname"        , "Yakın İsmi"},
            {"pare_lname"        , "Yakın Soyismi"},
            {"pare_sex"          , "Yakın Cinsiyeti"},
            {"pare_email"        , "Yakın E-Mail"},
            {"pare_number"       , "Yakın Telefon Numarası"},

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

            {"cour_hour"         , "Ders Saati"},
            {"cour_day"          , "Ders Günü"},

            {"avail_hour"        , "Uygun Olunan Saat"},
            {"avail_day"         , "Uygun Olunan Gün"},

            {"rela_type"         , "Akraba Yakınlık İlişkisi"},

            {"inst_id"           , "Ders Eğitmen ID"},

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

            {"Yakın ID"              , "pare_id"         },
            {"Yakın İsmi"            , "pare_fname"      },
            {"Yakın Soyismi"          , "pare_lname"      },
            {"Yakın Cinsiyeti"       , "pare_sex"        },
            {"Yakın E-Mail"          , "pare_email"      },
            {"Yakın Telefon Numarası" , "pare_number"     },

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

            {"Ders Saati"          , "cour_hour"},
            {"Ders Günü"           , "cour_day"},

            {"Uygun Olunan Saat"   , "avail_hour"      },
            {"Uygun Olunan Gün"    , "avail_day"       },

            {"Akraba Yakınlık İlişkisi", "rela_type"  },

            {"Ders Eğitmen ID"            , "inst_id"},

            {"İhtiyaç Duyulan Miktar", "need_amount"   },
        };

        public static List<ComboboxKeyValuePair> CATEGORY1_LIST = new List<ComboboxKeyValuePair>()
        {
            new ComboboxKeyValuePair("Öğrenci", "STUDENT", ""),
            new ComboboxKeyValuePair("Yakın", "PARENT", ""),
            new ComboboxKeyValuePair("Çalışan", "EMPLOYEE", ""),
            new ComboboxKeyValuePair("Ders", "COURSE", ""),
            new ComboboxKeyValuePair("Harcama", "EXPENSE", ""),
            new ComboboxKeyValuePair("Stok", "STOCK", ""),
        };

        public static Dictionary<string, List<ComboboxKeyValuePair>> CATEGORY1_TO_2_DICT = new Dictionary<string, List<ComboboxKeyValuePair>>()
        {
            {
                "STUDENT", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Öğrenciler", "STUDENT", ""),
                    new ComboboxKeyValuePair("Aktif Öğrenci", "ACTIVE", "JOIN ACTIVE USING(stud_id)"),
                    new ComboboxKeyValuePair("Mezun Öğrenci", "GRADUATE", "JOIN GRADUATE USING(stud_id)"),
                }
            },
            {
                "PARENT", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Yakınlar", "PARENT", ""),
                }
            },
            {
                "EMPLOYEE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Çalışanlar", "EMPLOYEE", ""),
                    new ComboboxKeyValuePair("Öğretim Görevlisi", "INSTRUCTOR", "JOIN INSTRUCTOR USING(empl_id)"),
                    new ComboboxKeyValuePair("Temizlik Görevlisi", "STAFF", "JOIN STAFF USING(empl_id)"),
                    new ComboboxKeyValuePair("İdari Personel", "ADMIN_STAFF", "JOIN ADMIN_STAFF USING(empl_id)"),
                }
            },
            {
                "COURSE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Dersler", "COURSE", ""),
                }
            },
            {
                "EXPENSE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Harcamalar", "EXPENSE", ""),
                }
            },
            {
                "STOCK", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Tüm Stoklar", "STOCK", ""),
                }
            },
        };

        public static Dictionary<string, List<ComboboxKeyValuePair>> CATEGORY2_TO_3_DICT = new Dictionary<string, List<ComboboxKeyValuePair>>()
        {
            {
                "STUDENT", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Yakınları", "RELATIVE", "JOIN RELATIVE USING(stud_id) JOIN PARENT USING(pare_id)"),
                }
            },
            {
                "ACTIVE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Yakınları", "RELATIVE", "JOIN RELATIVE USING(stud_id) JOIN PARENT USING(pare_id)"),
                    new ComboboxKeyValuePair("Müsait Zaman", "STUD_AVAIL_HOUR", "JOIN STUD_AVAIL_HOUR USING(stud_id)"),
                    new ComboboxKeyValuePair("Talep Edilen Ders", "REQUESTED_COURSE", "JOIN REQUESTED_COURSE USING(stud_id) JOIN COURSE USING(cour_id)"),
                    new ComboboxKeyValuePair("Alınan Ders", "TAKEN_COURSE", "JOIN TAKEN_COURSE USING(stud_id) JOIN COURSE USING(cour_id)"),
                    new ComboboxKeyValuePair("Alınan Ders Zamanları", "TAKEN_COURSE", "JOIN TAKEN_COURSE USING(stud_id) JOIN COURSE USING(cour_id) JOIN COUR_HOUR USING(cour_id)"),
                }
            },
            {
                "GRADUATE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Yakınları", "RELATIVE", "JOIN RELATIVE USING(stud_id) JOIN PARENT USING(pare_id)"),
                }
            },
            {
                "PARENT", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Öğrenci", "RELATIVE", "JOIN RELATIVE USING(pare_id) JOIN STUDENT USING(stud_id)"),
                }
            },
            {
                "EMPLOYEE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                }
            },
            {
                "INSTRUCTOR", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Müsait Zaman", "INST_AVAIL_HOUR", "JOIN INST_AVAIL_HOUR USING(empl_id)"),
                    new ComboboxKeyValuePair("Verdiği Ders", "COURSE", "JOIN GIVEN_COURSE ON INSTRUCTOR.empl_id=GIVEN_COURSE.inst_id JOIN COURSE USING(cour_id)"),
                    new ComboboxKeyValuePair("Ders Zamanı", "COUR_HOUR", "JOIN GIVEN_COURSE ON INSTRUCTOR.empl_id=GIVEN_COURSE.inst_id JOIN COURSE USING(cour_id) JOIN COUR_HOUR USING(cour_id)"),
                }
            },
            {
                "STAFF", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                }
            },
            {
                "ADMIN_STAFF", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                }
            },
            {
                "COURSE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Ders Eğitmeni", "INSTRUCTOR", "JOIN GIVEN_COURSE USING(cour_id) JOIN INSTRUCTOR ON GIVEN_COURSE.inst_id=INSTRUCTOR.empl_id JOIN EMPLOYEE USING(empl_id)"),
                    new ComboboxKeyValuePair("Ders Öğrencisi", "TAKEN_COURSE", "JOIN TAKEN_COURSE USING(cour_id) JOIN STUDENT USING(stud_id)"),
                    new ComboboxKeyValuePair("Ders Zamanı", "COUR_HOUR", "JOIN COUR_HOUR USING(cour_id)"),
                    new ComboboxKeyValuePair("Ders Materyali", "NEEDED_STOCK", "JOIN NEEDED_STOCK USING(cour_id) JOIN STOCK USING(stck_id)"),
                }
            },
            {
                "EXPENSE", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                }
            },
            {
                "STOCK", new List<ComboboxKeyValuePair>()
                {
                    new ComboboxKeyValuePair("Varsayılan", "", ""),
                    new ComboboxKeyValuePair("Kullanılan Ders", "NEEDED_STOCK", "JOIN NEEDED_STOCK USING(stck_id) JOIN COURSE USING(cour_id)"),
                }
            },
        };

        public static List<ComboboxKeyValuePair> NAME_TO_TABLE = new List<ComboboxKeyValuePair>()
        {
            new ComboboxKeyValuePair("Aktif Öğrenci", "STUDENT", "ACTIVE", "JOIN ACTIVE USING(stud_id)"),
            new ComboboxKeyValuePair("Aktif Öğrenci - Uygun Zaman", "STUD_AVAIL_HOUR", ""),
            new ComboboxKeyValuePair("Aktif Öğrenci - Talep Edilen Ders", "REQUESTED_COURSE", ""),
            new ComboboxKeyValuePair("Aktif Öğrenci - Alınan Ders", "TAKEN_COURSE", ""),
            new ComboboxKeyValuePair("Mezun Öğrenci", "STUDENT", "GRADUATE", "JOIN GRADUATE USING(stud_id)"),
            new ComboboxKeyValuePair("Öğrenci - Yakın", "RELATIVE", ""),

            new ComboboxKeyValuePair("Yakın", "PARENT", ""),
            new ComboboxKeyValuePair("Yakın - Öğrenci", "RELATIVE", ""),

            new ComboboxKeyValuePair("Öğretim Görevlisi", "EMPLOYEE", "INSTRUCTOR", "JOIN INSTRUCTOR USING(empl_id)"),
            new ComboboxKeyValuePair("Öğretim Görevlisi - Uygun Zaman", "INST_AVAIL_HOUR", ""),
            new ComboboxKeyValuePair("Öğretim Görevlisi - Verdiği Ders", "GIVEN_COURSE", ""),
            new ComboboxKeyValuePair("Temizlik Görevlisi", "EMPLOYEE", "STAFF", "JOIN STAFF USING(empl_id)"),
            new ComboboxKeyValuePair("İdari Personel", "EMPLOYEE", "ADMIN_STAFF", "JOIN ADMIN_STAFF USING(empl_id)"),

            new ComboboxKeyValuePair("Ders", "COURSE", ""),
            new ComboboxKeyValuePair("Ders - Zaman", "COUR_HOUR", ""),
            new ComboboxKeyValuePair("Ders - Öğretim Görevlisi", "GIVEN_COURSE", ""),
            new ComboboxKeyValuePair("Ders - Materyal", "NEEDED_STOCK", ""),
            new ComboboxKeyValuePair("Ders - Alan Öğrenci", "TAKEN_COURSE", ""),
            new ComboboxKeyValuePair("Ders - Talep Eden Öğrenci", "REQUESTED_COURSE", ""),

            new ComboboxKeyValuePair("Materyal", "STOCK", ""),
            new ComboboxKeyValuePair("Materyal - Ders", "NEEDED_STOCK", ""),

            new ComboboxKeyValuePair("Harcama", "EXPENSE", ""),
        };
    }
}
