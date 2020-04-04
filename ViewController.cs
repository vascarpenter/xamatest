using System;

using AppKit;
using Foundation;
using CoreText;
using CoreGraphics;
using ObjCRuntime;

namespace xamatest
{
    public partial class ViewController : NSViewController
    {
        private bool linefeed;
        private bool addunit;
        private int maxwidth = 70;

        // Xamarin mac only support  both CTStringAttributes and UIStringAttributes , but not support NSAttributedString.ForegroundColorAttributeName
        NSString keyForegroundColor = new NSString("NSColor"); // hard coded "NSAttributedString.ForegroundColorAttributeName"

        Mush[] data = new Mush[]
             {
                    new Mush("TP", "TP", "g/dl"),
                    new Mush("Alb", "Alb", "g/dl"),
                    new Mush("T-Bil", "T.Bil", "mg/dl"),
                    new Mush("D-Bil", "D.Bil", "mg/dl"),
                    new Mush("AST", "AST", "IU/l"),
                    new Mush("ALT", "ALT", "IU/l"),
                    new Mush("ALP", "ALP", "IU/l"),
                    new Mush("LDH", "LDH", "IU/l"),
                    new Mush("r-GTP", "γ-GTP", "IU/l"),
                    new Mush("LDH", "LDH", "IU/l"),
                    new Mush("Ch-E", "ChE", "IU/l"),
                    new Mush("AMY", "Amy", "IU/l"),
                    new Mush("P-AMY", "P-Amy", "IU/l"),                    // 20171010 追加
                    new Mush("CK", "CK", "IU/l"),
                    new Mush("CK-MB", "CK-MB", "IU/l"),
                    new Mush("e-GFR", "eGFR", "ml/min/BSA"),
                    new Mush("ZTT", "ZTT", ""),
                    new Mush("TTT", "TTT", ""),
                    new Mush("HDL-Cho", "HDL", "mg/dl"),
                    new Mush("LDL-Cho", "LDL", "mg/dl"),
                    new Mush("LDLｹｲｻﾝ", "LDL(calc)", "mg/dl"),
                    new Mush("TG", "TG", "mg/dl"),
                    new Mush("T-Cho", "T.Chol", "mg/dl"),
                    new Mush("BUN", "BUN", "mg/dl"),
                    new Mush("UA", "UA", "mg/dl"),
                    new Mush("CRE", "Cr", "mg/dl"),
                    new Mush("Na", "Na", "mEq/l"),
                    new Mush("K", "K", "mEq/l"),
                    new Mush("Cl", "Cl", "mEq/l"),
                    new Mush("Ca", "Ca", "mg/dl"),
                    new Mush("ｶﾙｼｳﾑ", "Ca", "mg/dl"),
                    new Mush("IP", "P", "mg/dl"),
                    new Mush("Mg", "Mg", "mg/dl"),                        // 20160531 変更
                    new Mush("NH3", "NH3", "µg/dl"),                        // 20150601 追加
                    new Mush("HbA1c(NGSP)", "HbA1c(NGSP)", "%"),            // 20131016 追加
                    new Mush("HbA1c(JDS)", "HbA1c(JDS)", "%"),            // 20131016 追加
                    new Mush("HbA1c", "HbA1c", "%"),
                    new Mush("DM HbA1c", "HbA1c", "%"),                    // 20150601 追加
                    new Mush("ＨｂＡ１ｃ", "HbA1c", "%"),
                    new Mush("血糖(空腹時）", "FBS", "mg/dl"),
                    new Mush("血糖(随時）", "BS", "mg/dl"),
                    new Mush("血糖(随時）", "BS", "mg/dl"),
                    new Mush("DMｹｯﾄｳ", "BS", "mg/dl"),                        // 20150601 追加
                    new Mush("血中ｹﾄﾝ", "ketone", "μmol/l"),                // 20160531 追加
                    new Mush("Glu", "BS", "mg/dl"),
                    new Mush("ＤＭ血糖", "BS", "mg/dl"),
                    new Mush("血沈 (至急) 1時間", "ESR(1h)", ""),
                    new Mush("血沈 (至急) 2時間", "ESR(2h)", ""),
                    new Mush("白血球", "WBC", "x10^3/μl"),
                    new Mush("白血球", "WBC", "x10^3/μl"),
                    new Mush("ﾍﾓｸﾞﾛﾋﾞﾝ", "Hb", "g/dl"),
                    new Mush("ﾍﾏﾄｸﾘｯﾄ", "Ht", "%"),
                    new Mush("ヘマトクリット", "Ht", "%"),
                    new Mush("血小板", "Plt", "x10^4/μl"),
                    new Mush("血小板", "Plt", "x10^4/μl"),
                    new Mush("ＭＣＶ", "MCV", ""),
                    new Mush("ＭＣＨ", "MCH", ""),
                    new Mush("網状赤血球", "Ret", "%%"),
                    new Mush("Neut%", "Neu", "%"),
                    new Mush("Lymph%", "Lym", "%"),
                    new Mush("Mono%", "Mono", "%"),
                    new Mush("Eos%", "Eos", "%"),
                    new Mush("Baso%", "Baso", "%"),
                    new Mush("ＰＴ-秒", "PT", "sec"),
                    new Mush("ＰＴ-INR", "PT-INR", ""),
                    new Mush("ＡＰＴＴ", "APTT", "sec"),
                    new Mush("AT㈽ ", "ATIII", ""),
                    new Mush("Fbg", "Fib", "mg/dl"),
                    new Mush("FDP(血中)", "FDP", "μg/mL"),
                    new Mush("CRP", "CRP", "mg/dl"),
                    new Mush("HBsAg", "HBsAg", ""),                        // 20160301変更
                    new Mush("HBsAb", "HBsAb", ""),                        // 20160322変更
                    new Mush("RPR", "RPR", ""),
                    new Mush("TPAb", "TPHA", ""),
                    new Mush("HCVAb", "HCVAb", "COI"),
                    new Mush("HIV", "HIV", ""),                            // 20160322変更
                    new Mush("HTLV-Ⅲ", "HTLV-III", ""),
                    new Mush("HTLV-ⅢCOI", "HTLV-III", ""),
                    new Mush("Ｄﾀﾞｲﾏｰ", "FDP-DD", "μg/mL"),                // 20160301変更
                    new Mush("ﾄﾛﾎﾟﾆﾝI", "TnI", "ng/ml"),                    // 20160301変更
                    new Mush("ＡＢＯ型", "Blood Type", ""),
                    new Mush("ＲＨ型", " ", ""),
                    new Mush("H-FABP", "H-FABP", "ng/mL"),
                    new Mush("蛋白", "U-protein", ""),
                    new Mush("潜血定性", "U-blood", ""),
                    new Mush("ｹﾄﾝ", "U-ketone", ""),
                    new Mush("ｳﾛﾋﾞﾘﾉｰｹ", "U-urobilinogen", ""),                // 20150601 追加
                    new Mush("糖", "U-sugar", ""),
                    new Mush("蛋白定性", "U-protein", ""),
                    new Mush("潜血定性", "U-blood", ""),
                    new Mush("Fe", "Fe", "μg/dL"),                        // 20160322 変更
                    new Mush("血清亜鉛", "Zn", "μg/dL"),
                    new Mush("網状赤血球数", "Ret", "%"),
                    new Mush("赤沈　1H", "ESR(1h)", "mm"),                    // 20170427 追加
                    new Mush("赤沈　2H", "ESR(2h)", "mm"),                    // 20170427 追加
                    new Mush("TIBC", "TIBC", "μg/dL"),
                    new Mush("UIBC", "UIBC", "μg/dL"),
                    new Mush("BNP", "BNP", "pg/ml"),
                    new Mush("NTproBNP", "NTproBNP", "pg/ml"),
                    new Mush("NT-proBNP", "NTproBNP", "pg/ml"),
                    new Mush("TSH", "TSH", "μIU/mL"),
                    new Mush("FT3", "fT3", "pg/mL"),
                    new Mush("FT4", "fT4", "ng/dL"),
                    new Mush("アドレナリン", "Adr", "pg/mL"),
                    new Mush("ｱﾄﾞﾚﾅﾘﾝ", "Adr", "pg/mL"),                    // 20170427 追加
                    new Mush("ﾉﾙｱﾄﾞﾚﾅﾘ", "NA", "pg/mL"),
                    new Mush("ﾄﾞｰﾊﾟﾐﾝ", "Dopa", "ng/mL"),                    // 20170427 追加
                    new Mush("ﾚﾆﾝ", "Renin", "ng/ml/h"),                    // 20170427 追加
                    new Mush("ｱﾙﾄﾞｽﾃﾛﾝ", "Aldosterone", "ng/dl"),            // 20170427 追加
                    new Mush("GNRI", "GNRI", ""),                            // 20171002 追加
                    new Mush("PSA", "PSA", "ng/mL"),
                    new Mush("ＰＳＡ", "PSA", "ng/mL"),
                    new Mush("AFP", "AFP", "ng/mL"),
                    new Mush("KL-6", "KL-6", "U/ml"),
                    new Mush("CEA", "CEA", "ng/mL"),
                    new Mush("CA19-9", "CA19-9", "U/mL"),
                    new Mush("CA125", "CA125", "U/mL"),                    // 20170125 追加
                    new Mush("CA15-3", "CA15-3", "U/mL"),                    // 20170125 追加
                    new Mush("NSE/ECLI", "NSE/ECLI", "ng/ml"),            // 20170316 追加
                    new Mush("sIL-2R", "sIL-2R", "U/ml"),                    // 20170316 追加
                    new Mush("ﾌｪﾘﾁﾝ", "Ferritin", "mg/ml"),
                    new Mush("A-溶連菌", "A群β溶連菌", ""),                    // 20170125 追加
                    new Mush("RSV抗原", "RSV抗原", ""),                        // 20170125 追加
                    new Mush("ﾏｲｺ抗原", "マイコプラズマ抗原", ""),                // 20170125 追加
                    new Mush("肺炎抗原", "肺炎球菌抗原", ""),                    // 20170125 追加
                    new Mush("尿中肺炎", "尿中肺炎球菌抗原", ""),                // 20170807 追加
                    new Mush("ｱﾃﾞﾉｳｨﾙｽ", "アデノウィルス", ""),                // 20170807 追加
                    new Mush("ｲﾝﾌﾙA", "インフルエンザA", ""),                // 20170807 追加
                    new Mush("ｲﾝﾌﾙB", "インフルエンザB", ""),                // 20170807 追加
                    new Mush("βｰDｸﾞﾙｶ", "βｰD glucan", "pg/ml"),            // 20170807 追加
                    new Mush("ｴﾝﾄﾞﾄｷｼﾝ", "エンドトキシン", "pg/ml"),            // 20170807 追加
                    new Mush("ﾋﾟﾛﾘAb量", "H-PyloriAb", "U/ml"),            // 20170316 追加
                    new Mush("ICA/IGG", "膵島細胞抗体", ""),                // 20170316 追加
                    new Mush("ﾌﾟﾛｶﾙｼﾄﾆ", "プロカルシトニン", "ng/ml"),            // 20170902 追加
                    new Mush("CCP抗体", "CCP抗体", "U/ml"),                    // 20170902 追加
                    new Mush("便潜血免疫法", "Feces OB", ""),
                    new Mush("ocﾍﾓ1", "Feces OB(1日)", ""),                // 20170316 追加
                    new Mush("ocﾍﾓ2", "Feces OB(2日)", ""),                // 20170316 追加
                    new Mush("ﾏｸﾞﾈｼｳﾑ", "Mg", "mg/dL"),
                    new Mush("無機ﾘﾝ", "P", "mg/dL"),
                    new Mush("尿中ﾚｼﾞｵﾈﾗ抗原", "尿中ﾚｼﾞｵﾈﾗ抗原", ""),
                    new Mush("尿中肺炎球菌抗原", "尿中肺炎球菌抗原", ""),
                    new Mush("ﾚｼﾞｵﾈﾗ", "レジオネラ", ""),                    // 20170427 追加
                    new Mush("尿中肺炎", "尿中肺炎球菌抗原", ""),                // 20170427 追加
                    new Mush("PH", "pH", ""),                                // 20150601 追加
                    new Mush("pH", "pH", ""),
                    new Mush("PCO2", "pCO2", "mmHg"),
                    new Mush("pCO2", "pCO2", "mmHg"),                        // 20150601 追加
                    new Mush("PO2", "pO2", "mmHg"),
                    new Mush("po2", "pO2", "mmHg"),                        // 20150601 追加
                    new Mush("Na+", "Na", "mEq/l"),
                    new Mush("K+", "K", "mEq/l"),
                    new Mush("Ca++", "Ca", "mg/dl"),
                    new Mush("Cl-", "Cl", "mEq/l"),
                    new Mush("Glucose", "BS", "mg/dl"),
                    new Mush("COHb", "COHb", "%"),
                    new Mush("BE(vt)", "BE", ""),
                    new Mush("ABE", "BE", ""),                            // 20150601 追加
                    new Mush("SO2", "SaO2", "%"),
                    new Mush("sO2", "SaO2", "%"),                            // 20150601 追加
                    new Mush("AaDpO2", "AaDO2", "mmHg"),                    // 20150601 追加
                    new Mush("tHb", "Hb", "g/dl"),
                    new Mush("Hct", "Ht", "%"),
                    new Mush("RF", "RF", ""),
                    new Mush("IgG", "IgG", "mg/dl"),
                    new Mush("IgA", "IgA", "mg/dl"),
                    new Mush("IgM", "IgM", "mg/dl"),
                    new Mush("IVｺﾗｰｹﾞﾝ", "Type4 Collagen", "ng/ml"),       // 20181006
                    new Mush("補体C3c", "補体C3c", "mg/dl"),
                    new Mush("補体C4", "補体C4", "mg/dl"),
                    new Mush("U-CRE", "U-Cre", "mg/dl"),                    // 20160229追加
                    new Mush("U-Na", "U-Na", "mEq/l"),                    // 20160229追加
                    new Mush("U-Cl", "U-Cl", "mEq/l"),                    // 20160229追加
                    new Mush("塩分摂取", "塩分摂取", "g/day"),                // 20160229追加
                    new Mush("尿中HCG", "U-hCG", ""),                        // 20160301追加 20170427変更
                    new Mush("ｼﾞｺﾞｷｼﾝ", "digoxin", "ng/ml"),                // 20160301追加
                    new Mush("HOMA-R", "HOMA-R", ""),                        // 20160322追加
                    new Mush("ｲﾝｽﾘﾝ", "insulin", "μU/ml"),                   // 20160322追加
                    new Mush("1･5-AG-", "1･5-AG", "μg/ml"),                // 20160322追加
                    new Mush("Cﾍﾟﾌﾟﾁﾄﾞ", "C-Peptide", "ng/ml"),             // 20160322追加
                    new Mush("FENa", "FENa", "%"),                         // 20160322追加
                    new Mush("ｲﾝｽﾘﾝ", "insulin", "μU/ml"),                  // 20160322追加
                    new Mush("CYFRA", "CYFRA", "ng/ml"),                   // 20180411追加
                    new Mush("ﾊﾞﾙﾌﾟﾛNa", "ﾊﾞﾙﾌﾟﾛNa", "μg/ml"),               // 20180930追加
                    new Mush("細菌", "細菌", ""),                           // 20180930追加
            };

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        /**
         * 指定したデリミタで文字列を分割し、 Stringの配列で取得することができるメソッド.
         *
         * @param value     分割対象文字列
         * @param delimiter デリミタ
         * @return 分割されたString配列
         */
        public static String[] StringToArray(string value, string delimiter)
        {
            var list = new System.Collections.Generic.List<string>();

            StringTokenizer stt = new StringTokenizer(value, delimiter);
            for (; ; )
            {
                if (!stt.hasMoreTokens())
                    break;
                String word = stt.nextToken();
                list.Add(word);
            }

            return list.ToArray();
        }


        public static String[] StringToArrayOnce(string value, string delimiter)
        {

            var list = new System.Collections.Generic.List<string>();

            StringTokenizer stt = new StringTokenizer(value, delimiter);
            if (!stt.hasMoreTokens())
                return null;
            String word = stt.nextToken();
            list.Add(word);
            if (stt.hasMoreTokens())
            {
                word = stt.nextToken();
                while (stt.hasMoreTokens())    // トークンのあるかぎり
                    word = word + delimiter + stt.nextToken();    // わざわざ切った物をつなげなおす
                list.Add(word);
            }
            return list.ToArray();
        }

        public string GetClipboardString()
        {
            NSPasteboard pasteboard = NSPasteboard.GeneralPasteboard;
            Class[] classArray = { new Class("NSString") };
            if (pasteboard.CanReadObjectForClasses(classArray, null))
            {
                NSObject[] objectsToPaste = pasteboard.ReadObjectsForClasses(classArray, null);
                NSString str2 = (NSString)objectsToPaste[0];
                return str2.ToString();
            }
            return "";            
        }

        /**
         * 半角（半角英数記号、半角ｶﾅ）か判断する.
         *
         * @param c 対象char
         * @return 半角（半角英数記号、半角ｶﾅ）の場合 true
         */
        public static bool IsHankaku(char c)
        {
            bool res = false;
            if (('\u0020' <= c && c <= '\u007e') || // 半角英数記号
                    ('\uff61' <= c && c <= '\uff9f'))
            { // 半角カタカナ
                res = true;
            }
            return res;
        }

        /**
         * 項目、値、H/L を解釈してHTML形式に変換します.
         *
         * @param koumoku
         * @return 変換された文字列。まったく変換不能なら""を返します。
         */
        public string ProcessOneLine(string koumoku, string koumokuValue, string judge)
        {
            String t = "";


            //System.out.println(koumoku+","+koumokuValue+","+judge);


            for (int i = 0; i < data.Length; i++)
            {
                Mush ii = data[i];
                if (ii.fromString.Equals(koumoku))
                {
                    String addvalue = ii.toString + " " + koumokuValue;
                    if (addunit)
                    {
                        addvalue += ii.unit; // unitを持っているときのみ追加
                    }

                    if (judge.Equals("H"))
                    {
                        t += "<r:" + addvalue + ">";
                    }
                    else if (judge.Equals("L"))
                    {
                        t += "<b:" + addvalue + ">";
                    }
                    else
                    {
                        t += addvalue;
                    }
                    t += " ";
                    break;
                }
            }
            return t;
        }



        public string ConvertClipboard()
        {
            string str = GetClipboardString();

            StringTokenizer st = new StringTokenizer(str, "\n");
            if (!st.hasMoreTokens())
            {
                return "";
            }
            String t2 = st.nextToken();
            if (t2.Length < 8 || !t2.Substring(0, 4).Equals("患者番号"))
            {
                return "";
            }
            st.nextToken();
            st.nextToken();
            st.nextToken();
            //st.nextToken();
            String t = "labo) ";
            for (; ; )
            {
                if (!st.hasMoreTokens())
                    break;
                String tt = st.nextToken();
                if (tt == null)
                    break;

                String[] st1 = StringToArrayOnce(tt, "(");
                if (st1.Length < 2)
                    continue;
                String koumoku = st1[0].Trim();
                String[] st2 = StringToArrayOnce(tt, ")");
                String koumokuRight = st2[1].Trim();

                if (koumokuRight.Equals("") || st2.Length < 2)
                    continue;
                String[] st3 = StringToArray(koumokuRight, " ");
                String koumokuValue = st3[0];
                String judge = "";
                if (st3.Length > 1)
                    judge = st3[1];

                // Added special rule 20181002
                //   special rule; 尿ｾｲﾌﾞﾝ1 or ｾｲﾌﾞﾝ2 or ｾｲﾌﾞﾝ3..7 ｹｯｶを１行に

                if (!koumokuValue.Equals("") && koumoku.StartsWith("ｾｲﾌﾞﾝ"))
                {
                    t += "<r:" + koumokuValue;
                    //read one more line

                    tt = st.nextToken();
                    if (tt == null)
                        break;
                    st2 = StringToArrayOnce(tt, ")");
                    koumokuRight = st2[1].Trim();
                    t += " " + koumokuRight + "> ";

                    continue;
                }
                t += ProcessOneLine(koumoku, koumokuValue, judge);

            }
            return t;
        }

        /**
         * tag文字列(タグとそうでないものが混在した)を解析して Paragraph形式とplain text形式に変更します.
         * 適切な改行を文字幅に応じて行います。 その際、<r:...>タグは文字幅に含めず
         *
         * @param text 解析するべき文字列
         * @return out Paragraph  Document形式　out String  text/plain,
         */
        public void ReformatText(String text, NSTextStorage storage, out String plain)
        {
            String t = text;
            String t0 = "";
            String t1 = "";

            int ilen = t.Length;
            int curwidth = 0;
            for (int i = 0; i < ilen;)
            {
                char ch = t[i];
                i++;
                if (ch == '<' && !Char.IsDigit(ch))
                { // ignore "<0.1" but skip "<r: ...>"
                  // skip braces

                    // flush t0
                    if (t0 != "")
                    {
                        storage.Append(new NSMutableAttributedString(t0));
                        t0 = "";
                    }
                    NSMutableDictionary dict = null;
                    ch = t[i++];
                    if (ch == 'r')
                    {
                        dict = new NSMutableDictionary() { { keyForegroundColor, NSColor.Red } };
                    }
                    else if (ch == 'b')
                    {
                        dict = new NSMutableDictionary() { { keyForegroundColor, NSColor.Blue } };
                    }
                    i++;
                    do
                    {
                        ch = t[i++];
                        if (ch == '>') break;
                        t0 += Char.ToString(ch);
                        t1 += Char.ToString(ch);
                        if (IsHankaku(ch))
                            curwidth++;
                        else
                            curwidth += 2;
                    } while (ch != '>' && i < ilen);
                    if (dict != null)
                    {
                        storage.Append(new NSMutableAttributedString(t0, dict));
                    }
                    else
                    {
                        storage.Append(new NSMutableAttributedString(t0));
                    }
                    t0 = "";
                    continue;
                }
                if (linefeed && ch == ' ' && i < ilen - 1
                        && !Char.IsDigit(t[i]) && curwidth > maxwidth)
                {
                    curwidth = 0;
                    if (t0 != "")
                    {
                        storage.Append(new NSMutableAttributedString(t0));
                        t0 = "";
                    }
                    storage.Append(new NSMutableAttributedString("\n"));
                    t1 += "\n";
                }
                else
                {
                    if (ch == 'ー' || ch == '—' || ch == '−' || ch == '－')
                        ch = '-';
                    t0 += Char.ToString(ch);
                    t1 += Char.ToString(ch);

                    if (IsHankaku(ch))
                        curwidth++;
                    else
                        curwidth += 2;
                }
            }

            if (t0 != "")
            {
                storage.Append(new NSMutableAttributedString(t0));
            }

            plain = t1; // plain text code


        }


        partial void clipboardConvertButton(NSObject sender)
        {
            string str = ConvertClipboard();
            string plain;

            addunit = addunitCheck.State == NSCellStateValue.On;
            linefeed = linefeedCheck.State == NSCellStateValue.On;

            if (str == "")
            {
                str = "病院データではありません！";
                NSMutableAttributedString attrstr = new NSMutableAttributedString(str, new NSMutableDictionary() { { keyForegroundColor, NSColor.Red } });
                outputTextView.TextStorage.SetString(attrstr);
            }
            else
            {
                outputTextView.TextStorage.SetString(new NSMutableAttributedString(""));        // clear storage

                ReformatText(str, outputTextView.TextStorage, out plain);

            }
        }
    }
}
