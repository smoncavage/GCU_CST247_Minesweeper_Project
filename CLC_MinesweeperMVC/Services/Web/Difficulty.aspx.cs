using CLC_MinesweeperMVC.Models;
using EO.Base.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Services.Web {
    public partial class WebForm1:System.Web.UI.Page {
       

        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void UpdatePanel(object sender, EventArgs e) {

        }

        protected void Button1_Click(object send, EventArgs e) {
            int difficulty = 0;
            RadioButton[] radbtns = { RadioButton1, RadioButton2, RadioButton3 };
            for(int i = 0; i<radbtns.Length; i++) {
                if(radbtns[i].Checked) {
                    difficulty=i+1;
                }
            }
            WebForm2.Difficulty=difficulty;
            Response.Redirect("Minesweep.aspx");
        }   

    }
}