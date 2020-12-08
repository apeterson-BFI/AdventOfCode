using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Adv2020
{
    public class Day29
    {
        public string[] lines;

        public List<Passport> Passports;

        public Passport CurrentPassport;

        public Day29()
        {
            lines = DayInput.readDayLines(29, true);
            Passports = new List<Passport>();
            CurrentPassport = new Passport();
        }

        public long getPart1Answer()
        {
            string[] blocks;
            string[] parts;
            string field;
            string val;

            foreach(var line in lines)
            {
                if(line == "")
                {
                    Passports.Add(CurrentPassport);
                    CurrentPassport = new Passport();
                }
                else
                {
                    blocks = line.Split(' ');

                    foreach(var block in blocks)
                    {
                        parts = block.Split(':');
                        field = parts[0];
                        val = parts[1];

                        switch (field)
                        {
                            case "byr": CurrentPassport.BirthYear = Parser.parseInt(val, 0, 4); break;
                            case "eyr": CurrentPassport.ExpirationYear = Parser.parseInt(val, 0, 4); break;
                            case "iyr": CurrentPassport.IssueYear = Parser.parseInt(val, 0, 4); break;
                            case "hgt": CurrentPassport.Height = val; break;
                            case "hcl": CurrentPassport.HairColor = val; break;
                            case "ecl": CurrentPassport.EyeColor = val; break;
                            case "pid": CurrentPassport.PassportID = val; break;
                            case "cid": CurrentPassport.CountryID = Parser.parseInt(val, 0); break;
                        }
                    }
                }
            }

            Passports.Add(CurrentPassport);

            using (var wr = File.CreateText("passports.txt"))
            {
                foreach (var p in Passports)
                {
                    wr.WriteLine(p.ToString());
                }
            }  

            return Passports.Count(p => p.Valid);
        }
    }

    public class Passport
    {
        public static Regex heightcm = new Regex(@"^(\d+)cm$");
        public static Regex heightinch = new Regex(@"^(\d+)in$");
        public static Regex haircolor = new Regex(@"^#([0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f])$");
        public static Regex passid = new Regex(@"^(\d\d\d\d\d\d\d\d\d)$");

        public int? BirthYear { get; set; }

        public int? IssueYear { get; set; }

        public int? ExpirationYear { get; set; }

        public string Height { get; set; }

        public string HairColor { get; set; }

        public string EyeColor { get; set; }

        public string PassportID { get; set; }

        public int? CountryID { get; set; }

        public override string ToString()
        {
            return string.Format("BY:{0} IY: {1} EY: {2} HGT: {3} HCL: {4} ECY: {5} PID: {6} CID: {7} VALID? {8}", BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportID, CountryID, Valid);
        }

        public bool Valid
        {
            get
            {
                bool by = BirthYear != null && BirthYear >= 1920 && BirthYear <= 2002;
                bool iy = IssueYear != null && IssueYear >= 2010 && IssueYear <= 2020;
                bool ey = ExpirationYear != null && ExpirationYear >= 2020 && ExpirationYear <= 2030;

                bool he;
                int heightQty;

                if (Height == null)
                    he = false;
                else
                {
                    Match cmm = heightcm.Match(Height);
                    Match inm = heightinch.Match(Height);

                    if(cmm.Success)
                    {
                        heightQty = Parser.parseInt(cmm.Groups[1].Value, 0);
                        he = heightQty >= 150 && heightQty <= 193;
                    }
                    else if(inm.Success)
                    {
                        heightQty = Parser.parseInt(inm.Groups[1].Value, 0);
                        he = heightQty >= 59 && heightQty <= 76;
                    }
                    else
                    {
                        he = false;
                    }
                }

                bool hcl = HairColor != null && haircolor.Match(HairColor).Success;

                bool ecl;

                if(EyeColor != null)
                {
                    switch (EyeColor)
                    {
                        case "amb": ecl = true; break;
                        case "blu": ecl = true; break;
                        case "brn": ecl = true; break;
                        case "gry": ecl = true; break;
                        case "grn": ecl = true; break;
                        case "hzl": ecl = true; break;
                        case "oth": ecl = true; break;
                        default: ecl = false; break;
                    }
                }
                else
                {
                    ecl = false;
                }

                bool pid = PassportID != null && passid.Match(PassportID).Success;

                return by && iy && ey && he && hcl && ecl && pid;
            }

        }
    }
}
