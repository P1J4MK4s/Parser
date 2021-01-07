using Microsoft.AspNetCore.Mvc;

namespace ParserCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public string[] InitPostfix()
        {
            string[] postfixs = {
                //Постфиксы кузова :
                "osnovanie_kuzova-3/" , "nastil_pola-4/", "kovriki_pola-5/", "stekloochistiteli-7/",
                "stekloomyvateli-8/", "peredok_avtobusa-10/", "lyuk_peredka_motornyiy-11/", "bokovina_levaya-13/",
                "bokovina_pravaya-14/", "okno_bokoviny-15/", "dverka_lyuka_zadka-17/", "detali_kryshi-19/",
                "dver_passajirskaya-21/", "mehanizm_otkryvaniya_passajirskih_dvereiy-22/","mehanizm_upravleniya_dvermi-23/",
                "dver_kabiny_voditelya-25/","zamok_dveri_kabiny_voditelya-26/","sidene_voditelya-28/",
                "sidenya__poruchni__podnojki-30/","sidene_dvuhmestnoe-31/","peregorodka_kabiny_voditelya-33/",
                "otoplenie_kuzova-35/","ventilyacii_kuzova-36/","kapot_dvigatelya-38/","bryzgoviki_koles-40/",
                //Постфиксы двигателя :
                "obshiiy_vid_dvigatelya-43/", "podveska_dvigatelya-44/","blok_cilindrov_i_golovki_bloka_cilindrov_dvigatelya-45/",
                "val_kolenchatyiy__porshni_i_shatuny-46/","raspredelitelnyiy_val__klapany_i_tolkateli-47/","gazoprovod-48/",
                "smazochnaya_emkost_i_maslopriemnik-49/","smazochnyiy_nasos_i_privod_raspredelitelya-50/",
                "radiator_smazochnoiy_sistemy-51/", "ventilyaciya_smazochnoiy_emkosti-52/","toplivnyiy_bak_i_toplivoprovod-54/",
                "toplivnyiy_otstoiynik-55/","toplivnyiy_nasos-56/","toplivnyiy_nasos-57/","karbyurator-58/",
                "upravlenie_akseleratorom-59/","vozdushnyiy_filtr-60/","pnevmocentrobejnyiy_ogranichitel_oborotov-61/",
                "filtr_tonkoiy_ochistki_topliva-62/","glushitel_i_podveska_trub-64/","radiator_ohlajdeniya-66/","vodyanoiy_nasos-67/",
                "natyajnoiy_rolik_ventilyatora-68/","ventilyator_radiatora-69/","promejutochnyiy_val_privoda_ventilyatora-70/",
                "shtorka_radiatora_ohlajdeniya-71/",
                //Постфиксы трансмиссии : 
                "sceplenie-74/","privod_vyklyucheniya_scepleniya-75/","cilindry_scepleniya_glavnyiy_i_rabochiiy-76/",
                "korobka_peredach-78/","mehanizm_pereklyucheniya_peredach-79/","distancionnoe_upravlenie_korobkoiy_peredach-80/",
                "kardannye_valy-82/","zadniiy_most__differencial_i_poluosi-84/",
                //Постфиксы ходовой части :
                "bamper_peredniiy-87/","bamper_zadniiy-88/","perednyaya_i_zadnyaya_buksirnye_proushiny-89/","perednyaya_ressora-91/",
                "amortizatory-92/","zadnyaya_ressora-93/","os_perednyaya_i_rulevye_tyagi-95/","kolesa_i_stupicy-97/",
                "derjatel_i_pod_emnik_zapasnogo_kolesa-98/",
                //Постфиксы механизмов управления : 
                "upravlenie_rulevoe-101/","gidrousilitel_rulevogo_upravleniya-102/","perednie_rabochie_tormoza-104/",
                "zadnie_rabochie_tormoza-105/","glavnyiy_cilindr_gidrotormozov-106/","truboprovody_gidrotormozov_i_vakuumnoiy_sistemy-107/",
                "stoyanochnyiy_tormoz-108/","gidrovakuumnyiy_usilitel_tormozov-109/",
                //Постфиксы электрооборудования :
                "shema_lektrooborudovaniya-112/","generator_p266-113/","akkumulyatornaya_batareya_i_ee_ustanovka-114/",
                "katushka_zajiganiya__raspredelitel__svechi_i_privoda_zajiganiya-115/","starter-116/","blok_vyklyuchatel-117/",
                "fara_fg_122ev-118/","plafon_osvesheniya_salona-119/","plafon_kabiny_voditelya-120/","podkapotnaya_lampa-121/",
                "perenosnaya_lampa-122/","fonar_osvesheniya_uchastka_zemli-123/","fonar_osvesheniya_nomernogo_znaka-124/",
                "signaly_zvukovye-125/","verhniiy_gabaritnyiy_fonar-126/","fara_protivotumannaya-127/","shitok_priborov-129/",
                //Постфиксы пренадлежностей : 
                "shoferskiiy_instrument-132/",
                //Постфиксы приложений
                "shema_ustanovki_podshipnikov_na_avtobuse-135/"
            };
            return postfixs;
        }
        [HttpGet]
        public string Get()
        {
            string[] postfixs = InitPostfix();
            int[] countOfTree = { 0, 0, 0, 3, 2, 2, 3, 1, 1, 5, 3, 1, 2, 2, 0, 10, 9, 1, 6, 0, 3, 3, 1, 1, 0, 3, 3, 1, 2, 0, 2, 6, 0, 16, 1, 0, 1, 1 };
            string outString = "";
            string path = "https://www.avtoall.ru/catalog/paz-20/avtobusy-36/paz_672m-393/";
            HtmlAgilityPack.HtmlWeb connection = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument page = connection.Load(path);
            int i = 0;
            int k = 0;
            foreach (var tree in page.DocumentNode.SelectNodes("//a[@href='#']"))
            {
                for (int j = 0; j < countOfTree[i]; j++, k++)
                {
                    HtmlAgilityPack.HtmlDocument subPage = connection.Load(path + postfixs[k]);
                    var number = subPage.DocumentNode.SelectSingleNode("//th[@class='number']");
                    if (number.InnerText != "Номер детали")
                    {
                        outString += "Номер детали : " + number.InnerText + "  ";
                    }
                    var name = subPage.DocumentNode.SelectSingleNode("//th[@class='name']");
                    if (name.InnerText != "Наименование")
                    {
                        outString += "Наименование : " + name.InnerText;
                    }
                    var count = subPage.DocumentNode.SelectSingleNode("//th[@class='count']");
                    if (count.InnerText != "Кол-во на модель")
                    {
                        outString += count.InnerText;
                    }
                    var image = subPage.DocumentNode.SelectSingleNode("//img[@id='picture_img']");
                    var srcImage = image.GetAttributeValue("src", "NULL");
                    outString += srcImage;


                    //HtmlAgilityPack.HtmlDocument sss = connection.Load("https://www.avtoall.ru/catalog/paz-20/avtobusy-36/paz_672m-393/stekloochistiteli-7/");
                    //var divpricelist = sss.DocumentNode.SelectSingleNode("//img[@class='lazy loaded']");
                    //var secndSource = divpricelist.GetAttributeValue("src", "NULL");
                    //outString += secndSource;


                    //foreach (var lazyImage in subPage.DocumentNode.SelectNodes("//img[@class='lazy loaded']"))
                    //{
                    //    if (lazyImage != null)
                    //    {
                    //        var srcLazyImage = lazyImage.GetAttributeValue("src", "NULL");
                    //        outString += srcLazyImage;
                    //    }
                    //}

                    //foreach (var price in subPage.DocumentNode.SelectNodes("//b[@class ='price-internet']"))
                    //{
                    //    outString += price.InnerHtml;
                    //}
                    foreach (var absent in subPage.DocumentNode.SelectNodes("//tr[@class='part']"))
                    {
                        outString += absent.InnerText + "\n";
                    }
                    outString += "\n";
                }
                outString += tree.InnerText + "\n";
                i++;
            }
            return outString;
        }
    }
}
