using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Ochilab.UI.Chart {
    public class OPerformanceLineChart : System.Windows.Forms.DataVisualization.Charting.Chart {


        int count = 0;

        //Y軸グラフの範囲
        private int maximumY=100;
        private int minimumY=0;

        // 取得データの履歴
        private int maxHistory = 40;

        public int MaxHistory {
            get { return maxHistory; }
            set { maxHistory = value; }
        }


        public int MinimumY {
            get { return minimumY; }
            set { minimumY = value; }
        }

        public int MaximumY {
            get { return maximumY; }
            set { maximumY = value; }
        }


        //ChartAreaCollection ChartAreas;
        private Queue<DataPoint> dataQue = new Queue<DataPoint>();
        
        int chartIndex = 0;



        //データを追加する
        public void queData(DataPoint value) {
            dataQue.Enqueue(value);
            // 履歴の最大数を超えていたら、古いものを削除する
            while (dataQue.Count > maxHistory) {
                dataQue.Dequeue();
            }
            
        }


        //表示
        public void DrawChart() {

            count++;

            foreach (Series s in this.Series) {
                s.Points.Clear();
                foreach (DataPoint dp in dataQue) {
                    s.Points.Add(dp);                   
                   // s.Points.Add(new DataPoint(0, value));
                }

            }
        }
        
        public OPerformanceLineChart() {
        
            this.BackColor = Color.Black;
        }



        private void setAxis(Axis axis) {
                // 軸のメモリラベルのフォントサイズ上限値を制限
                axis.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット
                axis.LabelStyle.ForeColor = Color.White;


                // 軸の色をセット
                axis.MajorGrid.Enabled = true;
                axis.MajorGrid.LineColor = ColorTranslator.FromHtml("#008242");
                axis.MinorGrid.Enabled = false;
                axis.MinorGrid.LineColor = ColorTranslator.FromHtml("#008242");
            }


        public void initGraph() {
            int i=0;
            DataPoint dp = new DataPoint();
            dp.SetValueXY(i.ToString(), i);
            // チャートに表示させる値の履歴を全て0クリア
            while (dataQue.Count <= maxHistory) {
                dataQue.Enqueue(dp);
                
            }

            ChartAreas[chartIndex].BackColor = Color.Transparent;

            // チャート表示エリア周囲の余白をカットする
            //ChartAreas[chartIndex].InnerPlotPosition.Auto = false;
            ChartAreas[chartIndex].InnerPlotPosition.Width = 100; // 100%
            ChartAreas[chartIndex].InnerPlotPosition.Height = 90;  // 90%(横軸のメモリラベル印字分の余裕を設ける)
            ChartAreas[chartIndex].InnerPlotPosition.X = 8;
            ChartAreas[chartIndex].InnerPlotPosition.Y = 0;

            // X,Y軸の表示方法を定義
            setAxis(ChartAreas[chartIndex].AxisY);
            setAxis(ChartAreas[chartIndex].AxisX);

            ChartAreas[chartIndex].AxisX.MinorGrid.Enabled = true;
            ChartAreas[chartIndex].AxisY.Maximum = maximumY;    
            ChartAreas[chartIndex].AxisY.Minimum = minimumY;

            this.AntiAliasing = AntiAliasingStyles.None;


            // 折れ線グラフとして表示
            Series[chartIndex].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            // 線の色を指定
            Series[chartIndex].Color = ColorTranslator.FromHtml("#00FF00");

            // 凡例を非表示,各値に数値を表示しない
            Series[chartIndex].IsVisibleInLegend = false;
            //Series[chartIndex].IsValueShownAsLabel = false;

        }

    }
}
