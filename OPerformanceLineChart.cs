using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Ochilab.UI.Chart {
    class OPerformanceLineChart : System.Windows.Forms.DataVisualization.Charting.Chart {

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
        private Queue<int> dataQue = new Queue<int>();

       
        
        int chartIndex = 0;



        public void queData(int value) {

            dataQue.Enqueue(value);
            // 履歴の最大数を超えていたら、古いものを削除する
            while (dataQue.Count > maxHistory) {
                dataQue.Dequeue();
            }

        }


        public void show() {
            foreach (Series s in this.Series) {
                s.Points.Clear();
                foreach (int value in dataQue) {
                    // データをチャートに追加
                    s.Points.Add(new DataPoint(0, value));
                }
            }
        }


        public OPerformanceLineChart() {
        
            this.BackColor = Color.Black;
            // X,Y軸情報のセット関数を定義
            Action<Axis> setAxis = (axisInfo) => {
                // 軸のメモリラベルのフォントサイズ上限値を制限
                axisInfo.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット
                axisInfo.LabelStyle.ForeColor = Color.White;

                // 軸タイトルの文字色をセット(今回はTitle未使用なので関係ないが...)
                axisInfo.TitleForeColor = Color.White;

                // 軸の色をセット
                axisInfo.MajorGrid.Enabled = true;
                axisInfo.MajorGrid.LineColor = ColorTranslator.FromHtml("#008242");
                axisInfo.MinorGrid.Enabled = false;
                axisInfo.MinorGrid.LineColor = ColorTranslator.FromHtml("#008242");
            };

            


          

            // チャートに表示させる値の履歴を全て0クリア
            while (dataQue.Count <= maxHistory) {
                dataQue.Enqueue(0);
            }
        }


        public void initGraph() {

            
            ChartAreas[chartIndex].BackColor = Color.Transparent;

            // チャート表示エリア周囲の余白をカットする
            ChartAreas[chartIndex].InnerPlotPosition.Auto = false;
            ChartAreas[chartIndex].InnerPlotPosition.Width = 100; // 100%
            ChartAreas[chartIndex].InnerPlotPosition.Height = 90;  // 90%(横軸のメモリラベル印字分の余裕を設ける)
            ChartAreas[chartIndex].InnerPlotPosition.X = 8;
            ChartAreas[chartIndex].InnerPlotPosition.Y = 0;

            ChartAreas[chartIndex].Name = "chartarea" + chartIndex;


            // X,Y軸情報のセット関数を定義
            Action<Axis> setAxis = (axisInfo) => {
                // 軸のメモリラベルのフォントサイズ上限値を制限
                axisInfo.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット
                axisInfo.LabelStyle.ForeColor = Color.White;

                // 軸タイトルの文字色をセット(今回はTitle未使用なので関係ないが...)
                axisInfo.TitleForeColor = Color.White;

                // 軸の色をセット
                axisInfo.MajorGrid.Enabled = true;
                axisInfo.MajorGrid.LineColor = ColorTranslator.FromHtml("#008242");
                axisInfo.MinorGrid.Enabled = false;
                axisInfo.MinorGrid.LineColor = ColorTranslator.FromHtml("#008242");
            };

            // X,Y軸の表示方法を定義
            setAxis(ChartAreas[chartIndex].AxisY);
            setAxis(ChartAreas[chartIndex].AxisX);

            ChartAreas[chartIndex].AxisX.MinorGrid.Enabled = true;
            ChartAreas[chartIndex].AxisY.Maximum = maximumY;    
            ChartAreas[chartIndex].AxisY.Minimum = minimumY;    


            // 折れ線グラフとして表示
            Series[chartIndex].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            // 線の色を指定
            Series[chartIndex].Color = ColorTranslator.FromHtml("#00FF00");

            // 凡例を非表示,各値に数値を表示しない
            Series[chartIndex].IsVisibleInLegend = false;
            Series[chartIndex].IsValueShownAsLabel = false;

        }

    }
}
