﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace genshin_sim
{
    public partial class fmCharacterList : Form
    {
        public fmCharacterList()
        {
            InitializeComponent();
        }

        public Waifu Waifu { get; private set; }
        public Image WaifuImage { get; private set; }
        private int[] images_index = new int[] 
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
            31, 32, 33, 34, 35, 36, 37, 38, 39 
        };
        private string[] raw_lines = Properties.Resources.characters.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        private List<Waifu> waifus;

        private void fmCharacterList_Load(object sender, EventArgs e)
        {
            waifus = WaifuFactory.Waifus.ToList();
            for (int i = 0; i < waifus.Count; i++)
            {
                this.lstCharacters.Items.Add(new ListViewItem(waifus[i].Name, images_index[i]));
            }

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedItems.Count > 0)
            {
                fmReturn();
            }
        }

        private void fmReturn()
        {
            this.DialogResult = DialogResult.OK;
            int index = lstCharacters.SelectedItems[0].Index;
            this.Waifu = waifus[index];
            this.WaifuImage = imCharacters.Images[images_index[index]];
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private ElementalType cvt_str2element(string raw)
        {
            switch (raw)
            {
                case "Hydro":
                    return ElementalType.Hydro;
                case "Cryo":
                    return ElementalType.Cryo;
                case "Electro":
                    return ElementalType.Electro;
                case "Anemo":
                    return ElementalType.Anemo;
                case "Geo":
                default:
                    return ElementalType.Geo;
            }
        }

        private string get_value(string section, string key)
        {
            for (int i = 0; i < raw_lines.Length; i++)
            {
                if (raw_lines[i] == $"[{section}]")
                {
                    for (int j = i; j < raw_lines.Length; j++)
                    {
                        if (raw_lines[j].StartsWith($"{key}="))
                        {
                            return raw_lines[j].Substring(key.Length + 1);
                        }
                    }
                    return "";
                }
            }
            return "";

        }

        private void lstCharacters_DoubleClick(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedItems.Count > 0)
            {
                fmReturn();
            }
        }

        private void lstCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
