using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace Minecraft_Randomizer
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        string mPath = "";
        string rPath = "";
        string version = "";
        string pPath = "";
        public Form1()
        {
            InitializeComponent();
        }
        string RandomFile(string pathd, string typed)
        {
            var files = Directory.GetFiles(pathd, typed);
            return files[r.Next(files.Length)];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                version = radioButton1.Text;
            } else if (radioButton2.Checked)
            {
                version = radioButton2.Text;
            } else if (radioButton3.Checked)
            {
                version = radioButton3.Text;
            } else if (radioButton4.Checked)
            {
                version = radioButton4.Text;
            } else if (radioButton5.Checked)
            {
                version = radioButton5.Text;
            } else if (radioButton6.Checked)
            {
                version = radioButton6.Text;
            } else if (radioButton7.Checked)
            {
                version = radioButton7.Text;
            } else if (radioButton8.Checked)
            {
                version = radioButton8.Text;
            }
            if (!Directory.Exists(mPath + "\\resourcepacks\\Randomized Resource Pack"))
            {
                FileInfo[] taskFiles = new DirectoryInfo(mPath + "\\versions").GetFiles(version + "*.jar", SearchOption.AllDirectories);
                var extract = taskFiles[0].FullName;
                Directory.CreateDirectory(mPath + "\\resourcepacks\\Randomized Resource Pack");
                rPath = mPath + "\\resourcepacks\\Randomized Resource Pack";
                using (StreamWriter sw = File.CreateText(rPath + "\\pack.mcmeta"))
                {
                    if (version == "1.6")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 1, \"description\" : \"Randomized Pack\" } }");

                    }
                    else if (version == "1.7")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 1, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.8")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 1, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.9")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 2, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.10")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 2, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.11")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 3, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.12")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 3, \"description\" : \"Randomized Pack\" } }");
                    }
                    else if (version == "1.13")
                    {
                        sw.WriteLine("{ \"pack\" : { \"pack_format\" : 4, \"description\" : \"Randomized Pack\" } }");
                    }
                }
                Directory.CreateDirectory(rPath + "\\temp");
                ZipFile.ExtractToDirectory(extract, rPath + "\\temp");
                Directory.Move(rPath + "\\temp\\assets", rPath + "\\assets");
                Directory.Delete(rPath + "\\temp", true);
            }
            rPath = mPath + "\\resourcepacks\\Randomized Resource Pack";
            if (checkBox1.Checked)
            {
                var blockspath = version == "1.13" ? rPath + "\\assets\\minecraft\\textures\\block" : rPath + "\\assets\\minecraft\\textures\\blocks";
                foreach (string fil in Directory.GetFiles(blockspath, "*.png"))
                {
                    for (int i = 1; i <= 3; ++i)
                    {
                        try
                        {
                            if (pPath == "")
                            {
                                File.Copy(RandomFile(blockspath, "*.png"), fil, true);
                            } else
                            {
                                File.Copy(RandomFile(pPath, "*.png"), fil, true);
                            }
                            break; // When done we can break loop
                        }
                        catch (IOException r)
                        {
                            // You may check error code to filter some exceptions, not every error
                            // can be recovered.
                            if (i == 3) // Last one, (re)throw exception and exit
                                throw;

                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    
                }
            }
            if (checkBox2.Checked)
            {
                var itempath = version == "1.13" ? rPath + "\\assets\\minecraft\\textures\\item" : rPath + "\\assets\\minecraft\\textures\\items";
                foreach (string fil in Directory.GetFiles(itempath, "*.png"))
                {
                    for (int i = 1; i <= 3; ++i)
                    {
                        try
                        {
                            if (pPath == "")
                            {
                                File.Copy(RandomFile(itempath, "*.png"), fil, true);
                            }
                            else
                            {
                                File.Copy(RandomFile(pPath, "*.png"), fil, true);
                            }
                            break; // When done we can break loop
                        }
                        catch (IOException r)
                        {
                            // You may check error code to filter some exceptions, not every error
                            // can be recovered.
                            if (i == 3) // Last one, (re)throw exception and exit
                                throw;

                            System.Threading.Thread.Sleep(1000);
                        }
                    }

                }
            }
            if (checkBox3.Checked)
            {
                var is113 = version == "1.13";
                if (is113)
                {
                    int iddd = 0;
                    string[] lines = File.ReadAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.json");
                    foreach (string line in lines)
                    {
                        if (line.Length > 5 && !line.Contains("%s"))
                        {
                            if (line.Contains("item.minecraft"))
                            {
                                var ind = line.IndexOf(": ");
                                var bef = line.Substring(0, ind);
                                var rand = lines[r.Next(672, 1235)];
                                lines[iddd] = bef + rand.Substring(rand.IndexOf(": "));
                            }
                            else if (line.Contains("block.minecraft"))
                            {
                                var ind = line.IndexOf(": ");
                                var bef = line.Substring(0, ind);
                                var rand = lines[r.Next(1236, 1577)];
                                lines[iddd] = bef + rand.Substring(rand.IndexOf(": "));
                            }
                            iddd++;
                        }

                    }
                    File.WriteAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.json", lines);
                }
                else
                {
                    //////////////////////////////// start at 515 to 1156: item 896 to 1156: block 515 to 891
                    int iddd = 0;
                    string[] lines = File.ReadAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.lang");
                    foreach (string line in lines)
                    {
                        if (line.Length > 5 && !line.Contains("%s"))
                        {
                            if (line.Substring(0, 4).Equals("item"))
                            {
                                var ind = line.IndexOf("=");
                                var bef = line.Substring(0, ind);
                                var rand = lines[r.Next(896, 1156)];
                                lines[iddd] = bef + rand.Substring(rand.IndexOf("="));
                            }
                            else if (line.Substring(0, 4).Equals("tile"))
                            {
                                var ind = line.IndexOf("=");
                                var bef = line.Substring(0, ind);
                                var rand = lines[r.Next(515, 891)];
                                lines[iddd] = bef + rand.Substring(rand.IndexOf("="));
                            }
                            iddd++;
                        }
                        
                    }
                    File.WriteAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.lang", lines);
                }


                ////////////////////////////////
            }
            if (checkBox4.Checked)
            {
                var is113 = version == "1.13";
                if (is113)
                {
                    int iddd = 0;
                    string[] lines = File.ReadAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.json");
                    foreach (string line in lines)
                    {
                        if (line.Length > 5)
                        {
                            var ind = line.IndexOf(": ");
                            var bef = line.Substring(0, ind);
                            var rand = lines[r.Next(1, lines.Length - 2)];
                            lines[iddd] = bef + rand.Substring(rand.IndexOf(": "));

                        } else
                        {
                            Console.WriteLine("OJI");
                        }
                        iddd++;
                    }
                    
                    File.WriteAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.json", lines);
                }
                else
                {
                    int iddd = 0;
                    string[] lines = File.ReadAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.lang");
                    foreach (string line in lines)
                    {
                        if (line.Length > 5)
                        {
                            var ind = line.IndexOf("=");
                            var bef = line.Substring(0, ind);
                            var rand = lines[r.Next(1, lines.Length-2)];
                            lines[iddd] = bef + rand.Substring(rand.IndexOf("="));

                        }
                        iddd++;
                    }
                    
                    File.WriteAllLines(rPath + "\\assets\\minecraft\\lang\\en_us.lang", lines);
                }
                    //////////////////////////////// start at 515 to 1156: item 896 to 1156: block 515 to 891
                
            }




            ////////////////////////////////
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            mPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft";
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pPath = folderBrowserDialog1.SelectedPath;
                }
            } else if (checkBox5.Checked == false)
            {
                pPath = "";
            }
        }
    }
}
