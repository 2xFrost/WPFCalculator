using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        //funcka odpowiadająca za przyciski operacji [+, -, /, *, =]
        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            string currentButton = (string)button.Content;

            string[] possibleMathOperations = new string[] { "+", "-", "*", "/", "NWW", "NWD" };

            if (CurrentOperation.Text == string.Empty)
            {
                CurrentOperation.Text = string.Empty;
            }
            else if (possibleMathOperations.Any(CurrentOperation.Text.Contains) && currentButton != "=")
            {
                CurrentOperation.Text = CurrentOperation.Text;
            }
            else
            {
                switch (currentButton)
                {
                    case "NWW":
                        CurrentOperation.Text += " NWW ";
                        break;

                    case "NWD":
                        CurrentOperation.Text += " NWD ";
                        break;

                    case ",":
                        CurrentOperation.Text += ",";
                        break;

                    case "+":
                        CurrentOperation.Text += " + ";
                        break;
                    case "-":
                        CurrentOperation.Text += " - ";
                        break;
                    case "/":
                        CurrentOperation.Text += " / ";
                        break;
                    case "*":
                        CurrentOperation.Text += " * ";
                        break;
                    case "=":
                        DoMath(CurrentOperation.Text);
                        break;
                }
            }


        }

        // Funkcja odpowiadająca za przyciski [C, CE oraz X]

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            string currentButton = button.Name;
            int last_element_index = CurrentOperation.Text.Length - 1;


            if (currentButton == "ButtonDeleteCurrent")
            {
                CurrentOperation.Text = string.Empty;
            }
            else if (currentButton == "ButtonDeleteResult")
            {
                Result.Text = string.Empty;
                CurrentOperation.Text = string.Empty;
            }
            else
            {
                if (last_element_index > -1)
                {
                    if(CurrentOperation.Text.LastIndexOf(" ") == last_element_index)
                    {
                        CurrentOperation.Text.Trim();
                        CurrentOperation.Text = CurrentOperation.Text.Substring(0, last_element_index-2);
                        
                    }                
                    else
                    {
                        CurrentOperation.Text = CurrentOperation.Text.Substring(0, last_element_index);
                    }
                }
                else
                {
                    CurrentOperation.Text += " ";
                }
            }

        }

        //funkcja sprawdza który przycisk [0-9] został użyty i dodaje jego wartość do ekranu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            string currentButton = button.Name[button.Name.Length - 1].ToString();

            if (currentButton == "0" && CurrentOperation.Text.Length == 1 && CurrentOperation.Text.Contains("0"))
            {
                CurrentOperation.Text = CurrentOperation.Text;
            }
            else
            {
                CurrentOperation.Text += currentButton;
            }

        }

        //funkcja odpowiada za działania matematyczne w momencie wywołania [ +, -, /, *, NWD, NWW ]

        private void DoMath(string text)
        {
            text.Trim();
            string[] indexes = text.Split(" ");
            decimal a = 0;
            decimal b = 0;

            if (indexes.Length > 2 && indexes[2] != string.Empty)
            {
                a = decimal.Parse(indexes[0]);
                b = decimal.Parse(indexes[2]);

                Result.Text = CurrentOperation.Text;

                switch (indexes[1])
                {
                    case "+":

                        CurrentOperation.Text = (a + b).ToString();
                        break;

                    case "-":

                        CurrentOperation.Text = (a - b).ToString();
                        break;

                    case "*":

                        CurrentOperation.Text = (a * b).ToString();
                        break;

                    case "/":
                        CurrentOperation.Text = (a / b).ToString();
                        break;

                    case "NWW":
                        Nww(a, b);
                        break;

                    case "NWD":
                        Nwd(a, b);
                        break;
                }
            }
            else
            {
                CurrentOperation.Text = CurrentOperation.Text;
            }
            
        }
    
        //funkcja znajdueje i wyświetla największą wspólną wielokrotność podanych liczb 
        private void Nww(decimal a, decimal b)
        {
            int c = (int)a;
            int d = (int)b;
            
            int nww = c * d / FindNwd(c, d);
            CurrentOperation.Text = nww.ToString();
            
        }
        //funkcja znajdueje najmniejszy wspólny dzielnik podanych liczb 
        private int FindNwd(int a, int b)
        {
           
            while ( b != 0 )
            {
                int temp = a % b;
                a = b;
                b = temp;
            }
            return a;        
        }
        //funkcja wyświetla najmniejszy wspólny dzielnik podanych liczb 
        private void Nwd(decimal a, decimal b)
        {
            int c = (int)a;
            int d = (int)b;
            
            CurrentOperation.Text = FindNwd(c, d).ToString();
        }
    }
}
