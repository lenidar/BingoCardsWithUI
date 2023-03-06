using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BingoCardsWithUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Label[] labels = new Label[5];
        Button[,] things = new Button[5, 5];
        Button[] cards = new Button[] { };
        int[][,] bingoValues = new int[][,] { };
        Random rnd = new Random();

        double margin = 10;
        double size = 50;

        double[] drawLocation = { 0, 0 }; // 0: vertical, 1: horizontal

        public MainWindow()
        {
            cards = new Button[rnd.Next(2, 5)];
            bingoValues = new int[cards.Length][,];

            InitializeComponent();

            generateBingoCards();

            drawLocation[0] += margin;
            drawLocation[1] += margin;

            string[] headers = { "B", "I", "N", "G", "O" };

            // generates controls
            // genenerating the labesl
            for(int x = 0; x < labels.Length; x++)
            {
                labels[x] = generateLabel(drawLocation[0], drawLocation[1], headers[x]);
                drawLocation[1] += margin + size;
            }

            drawLocation[0] += margin + size;
            drawLocation[1] = margin;
            // generate the buttons
            for (int x = 0; x < things.GetLength(0); x++)
            {
                for(int y = 0; y < things.GetLength(1); y++)
                {
                    things[x, y] = generateButton(drawLocation[0], drawLocation[1], "000");
                    drawLocation[1] += margin + size;
                }
                drawLocation[0] += margin + size;
                drawLocation[1] = margin;
            }
            // generate new buttons with event
            drawLocation[0] = 70;
            drawLocation[1] = 350;
            for(int x = 0; x < cards.Length; x++)
            {
                cards[x] = generateButton(drawLocation[0], drawLocation[1], (x + 1) + "");
                cards[x].IsEnabled = true;
                cards[x].Click += genericClick;
                drawLocation[0] += size + margin;
            }

            // display the controls
            // display the labels
            for (int x = 0; x < labels.Length; x++)
                myGrid.Children.Add(labels[x]);
            // disp  the buttons
            for (int x = 0; x < things.GetLength(0); x++)
            {
                for (int y = 0; y < things.GetLength(1); y++)
                {
                    myGrid.Children.Add(things[x,y]);
                }
            }
            // display the new buttons
            for (int x = 0; x < cards.Length; x++)
                myGrid.Children.Add(cards[x]);
        }

        private void genericClick(object sender, RoutedEventArgs e)
        {
            int cardSelect = 0;

            // enable others
            for (int x = 0; x < cards.Length; x++)
                cards[x].IsEnabled = true;
            // disable itself
            ((Button)sender).IsEnabled = false;

            // select the card
            cardSelect = int.Parse(((Button)sender).Content.ToString()) - 1;

            // feed the card to the button display
            for(int x = 0; x < things.GetLength(0); x++)
            {
                for(int y = 0; y < things.GetLength(1); y++)
                {
                    things[x, y].Content = bingoValues[cardSelect][x, y];
                }
            }
        }

        private Button generateButton(double topMargin, double leftMargin, string content)
        {
            Button button = new Button();

            button.Width = size;
            button.Height = size;

            button.Margin = new Thickness(leftMargin,topMargin,0, 0);
            button.VerticalAlignment = VerticalAlignment.Top;
            button.HorizontalAlignment = HorizontalAlignment.Left;

            button.Content = content;

            button.IsEnabled = false;

            return button;
        }

        private Label generateLabel(double topMargin, double leftMargin, string content)
        {
            Label label = new Label();

            label.Content = content;
            label.FontSize = 28;
            label.Height = size;
            label.Width = size;
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.Margin = new Thickness(leftMargin, topMargin, 0, 0);
            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            return label;
        }

        private void generateBingoCards()
        {
            int temp = 0;
            for (int x = 0; x < bingoValues.Length; x++) 
            {
                bingoValues[x] = new int[5, 5];
                for(int y = 0; y < bingoValues[x].GetLength(0); y++) // row
                {
                    for(int z = 0; z < bingoValues[x].GetLength(1); z++) // col
                    {
                        temp = rnd.Next(1, 16);
                        temp += (15 * z);
                        bingoValues[x][y, z] = temp;
                    }
                }
            }
        }
    }
}
