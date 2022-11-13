using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace Struct6Dot6
{
    sealed public partial class MainWindow : Window
    {
        private readonly string _url = "https://randomus.ru/name?type=0&sex=10&count=100";
        private string[] _fio = new string[3];
        private int countOfStudents = 0;
        private string[] allStudents = new string[100];
        private int _allStudents;

        XmlDoc xdoc = new XmlDoc();
        public Student[]? students;
        public Lesson[]? lessons;
        

        public MainWindow()
        {
            
            InitializeComponent();
            sqlDoc.Clear();
            Random random = new Random();
            textboxHowMutch.Text = random.Next(10, 10000).ToString();
            _fio = "no internet connection".Split(" ");
            buttonSql.Visibility = Visibility.Hidden;
            panelXml.Visibility = Visibility.Hidden;
            panelLessons.Visibility = Visibility.Hidden;
            secondColumn.Width = fourColumn.Width;
            thirdColumn.Width = fourColumn.Width;
            window.Width = 280;
            NewStudentWeb();
            lessons = new Lesson[5];
            lessons[0].Name = "Физика";
            lessons[1].Name = "Математика";
            lessons[2].Name = "Программирование";
            lessons[3].Name = "Английский";
            lessons[4].Name = "Химия";

            comboboxSql.Items.Add("Кол-во учеников с оценкой от 4");
            comboboxSql.Items.Add("Кол-во учеников с оценкой ниже 4");
            comboboxSql.Items.Add("Кол-во учеников");
            comboboxSql.Items.Add("Кол-во учеников с повышенной стипендией");
            comboboxSql.Items.Add("Сортировка");
            comboboxSql.Items.Add("Те у кого от 4");

            buttonHowMuch.Click += buttonHowMuch_Click;
            buttonSaveHomMuch.Click += buttonSaveHomMuch_Click;
        }

        private void buttonSaveHomMuch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int check = Convert.ToInt32(textboxHowMutch.Text);
                textboxHowMutch.IsEnabled = false;
                buttonHowMuch.IsEnabled = false;
                panelInfo.Visibility = Visibility.Visible;

                panelHowMuch.MouseEnter -= panelHowMuch_MouseEnter;
                panelHowMuch.MouseLeave -= panelHowMuch_MouseLeave;

                students = new Student[int.Parse(textboxHowMutch.Text)];

                panelHowAnimation();

                secondColumn.Width = firstColumn.Width;
                thirdColumn.Width = firstColumn.Width;
                buttonSql.Visibility = Visibility.Visible;
                panelXml.Visibility = Visibility.Visible;
                panelLessons.Visibility = Visibility.Visible;

                MainAnimation();
                panelXmlAnimation();
                addOnForm();
                _allStudents = int.Parse(textboxHowMutch.Text);
            }
            catch 
            {
                textboxHowMutch.Text = "Введите число!";
            }
        }
        private void buttonHowMuch_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            textboxHowMutch.Text = random.Next(10, 10000).ToString();
        }
        private void panelHowMuch_MouseEnter(object sender, MouseEventArgs e)
        {
            panelHowAnimation(true);
        }
        private void panelHowMuch_MouseLeave(object sender, MouseEventArgs e)
        {
            panelHowAnimation();
        }

        private void buttonXml_Click(object sender, RoutedEventArgs e)
        {
            AddInXml(1);
            addOnForm();

        }
        private async void buttonAutoXml_Click(object sender, RoutedEventArgs e)
        {
            AddInXml((int.Parse(textboxHowMutch.Text)-xdoc.CountOfElements()) <= 0 ? 1 : (int.Parse(textboxHowMutch.Text) - xdoc.CountOfElements()));
            Grid.ItemsSource = await Task.Run(() => DataGridAdd());
        }
        private async void buttonOpenXml_Click(object sender, RoutedEventArgs e)
        {
            Grid.ItemsSource = await Task.Run(() => DataGridAdd());
        }

        private void panelHowAnimation(bool UpAndDown = false)
        {
            var doubleAnimation = new DoubleAnimation();
            var thicnessAnimationText = new ThicknessAnimation();
            var thicnessAnimationRnd = new ThicknessAnimation();
            var thicnessAnimationSave = new ThicknessAnimation();

            thicnessAnimationText.From = textboxHowMutch.Margin;
            doubleAnimation.From = panelHowMuch.Height;
            thicnessAnimationRnd.From = buttonHowMuch.Margin;
            thicnessAnimationSave.From = buttonSaveHomMuch.Margin;

            if (UpAndDown)
            {
                doubleAnimation.To = 400;
                doubleAnimation.Duration = TimeSpan.FromSeconds(2);
                doubleAnimation.DecelerationRatio = 1;

                thicnessAnimationText.To = new Thickness(10, 41, 10, 41);
                thicnessAnimationText.Duration = TimeSpan.FromSeconds(2);
                thicnessAnimationText.DecelerationRatio = 1;

                thicnessAnimationRnd.To = new Thickness(10, 38, 10, 38);
                thicnessAnimationRnd.Duration = TimeSpan.FromSeconds(2.5);
                thicnessAnimationRnd.DecelerationRatio = 1;

                thicnessAnimationSave.To = new Thickness(10, 38, 10, 38);
                thicnessAnimationSave.Duration = TimeSpan.FromSeconds(3);
                thicnessAnimationSave.DecelerationRatio = 1;
            }
            else
            {
                doubleAnimation.To = 108;
                doubleAnimation.Duration = TimeSpan.FromSeconds(1);
                doubleAnimation.AccelerationRatio = 1;

                thicnessAnimationText.To = new Thickness(10, 41, 250, 41);
                thicnessAnimationText.Duration = TimeSpan.FromSeconds(1);
                thicnessAnimationText.AccelerationRatio = 1;

                thicnessAnimationRnd.To = new Thickness(10, 38, 250, 38);
                thicnessAnimationRnd.Duration = TimeSpan.FromSeconds(1.5);
                thicnessAnimationRnd.DecelerationRatio = 0.5;

                thicnessAnimationSave.To = new Thickness(10, 38, 250, 38);
                thicnessAnimationSave.Duration = TimeSpan.FromSeconds(1);
                thicnessAnimationSave.DecelerationRatio = 0.2;
            }

            panelHowMuch.BeginAnimation(Panel.HeightProperty, doubleAnimation);
            textboxHowMutch.BeginAnimation(TextBox.MarginProperty,thicnessAnimationText);
            buttonHowMuch.BeginAnimation(Button.MarginProperty,thicnessAnimationRnd);
            buttonSaveHomMuch.BeginAnimation(Button.MarginProperty,thicnessAnimationSave);
        }
        private void MainAnimation()
        {
            var doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 280;
            doubleAnimation.To = 800;
            doubleAnimation.DecelerationRatio = 1;
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);
            window.BeginAnimation(Window.WidthProperty, doubleAnimation);
        }
        private void panelXmlAnimation()
        {
            var thicnessAnimation = new ThicknessAnimation();
            thicnessAnimation.From = new Thickness(10, 10, 200, 0);
            thicnessAnimation.To = new Thickness(10, 10, 10, 0);
            thicnessAnimation.Duration = TimeSpan.FromSeconds(2);
            thicnessAnimation.DecelerationRatio = 1;
            buttonXml.BeginAnimation(Button.MarginProperty,thicnessAnimation);
            thicnessAnimation.Duration = TimeSpan.FromSeconds(2.2);
            buttonAutoXml.BeginAnimation(Button.MarginProperty, thicnessAnimation);
            thicnessAnimation.Duration = TimeSpan.FromSeconds(2.4);
            buttonOpenXml.BeginAnimation(Button.MarginProperty, thicnessAnimation);

        }
        
        public void  NewStudentWeb()
        {
            var client = new WebClient();
            client.Proxy = new WebProxy();
            client.DownloadFile(_url, "Students");
            using (StreamReader sr = File.OpenText("Students"))
            {
                string s = String.Empty;
                int index = 0;

                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("                                <span class=\"has-text-weight-bold is-size-4 is-size-5-mobile is-size-3-widescreen \">"))
                    {
                        string name = s.Substring(116);
                        allStudents[index] = name.Substring(0, name.Length - 7);
                        index++;
                    }
                }
            }
        }

        private void addOnForm()
        {
            if (countOfStudents == 99)
            {
                countOfStudents = 0;
                NewStudentWeb();
            }

            _fio = allStudents[countOfStudents].Split(" ");
            textSurname.Text = _fio[0];
            textName.Text = _fio[1];
            textPatronymic.Text = _fio[2];

            countOfStudents++;
            Random random = new Random();
            foreach (UIElement textBox in lesson.Children)
            {
                if (textBox is TextBox)
                {
                    ((TextBox)textBox).Text = random.Next(2, 6).ToString();
                }
            }
        }
        private void add()
        {
            if (countOfStudents == 99)
            {
                countOfStudents = 0;
                Thread.Sleep(5000);
                NewStudentWeb();
            }
            _fio = allStudents[countOfStudents].Split(" ");
            countOfStudents++;
        }
        private async void AddInXml(int counter)
        {
            for (int i = 0; i <= counter; i++)
            {
                await Task.Run(() => adding(i, false));
                if (xdoc.CountOfElements() >= int.Parse(textboxHowMutch.Text))
                {
                    panelLessons.Visibility = Visibility.Hidden;
                    Grid.Visibility = Visibility.Visible;
                    xdoc.Save();
                    Grid.ItemsSource = await Task.Run(() => DataGridAdd());
                    break;
                }
            }
        }
        private void adding(int i = 0,bool text = true)
        {
            if (text)
            {
                students[0] = new Student(textSurname.Text, textName.Text, textPatronymic.Text);
            }
            else
            {
                students[0] = new Student(_fio[0], _fio[1], _fio[2]);
            }
            Random random = new Random();
            for (int j = 0; j < 5; j++)
            {
                lessons[j].Point = random.Next(2, 6);
            }
            Group group = new Group();
            xdoc.Add(students[0], lessons, _allStudents - i,group);
            add();

        }
        private List<GridStudent> DataGridAdd()
        {
            CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            XDocument document = XDocument.Load("StudentsXml");
            List<GridStudent> result = new List<GridStudent>();
            int count = 0;
            for (int i = 1; i < xdoc.CountOfElements(); i++)
            {
                try
                {
                    XElement student = document.Element("Students")
                                                .Elements("Id")
                                                .Single(x => (int?)x.Attribute("id") == i);

                    result.Add(new GridStudent(i, student.Element("Surname").Value, student.Element("Name").Value, student.Element("Patronymic").Value, student.Element("Group").Value, student.Element("Lessons").Attribute("avg").Value));
                }
                catch 
                {
                }
            }
            return result;
        }
        SqlDoc sqlDoc = new SqlDoc();
        Random rnd = new Random();
        
        private async void buttonSql_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => AddIntoSql());
            labelHowMuch.Content = "База данных заполнена!";
        }
        private void AddIntoSql()
        {
            for (int i = 0; i < _allStudents; i++)
            {
                double avg = 0;
                for (int j = 0; j < 5; j++)
                {
                    avg += rnd.Next(2, 6);
                }

                avg /= 5.0;
                Group group = new Group();
                sqlDoc.Add(new Student(_fio[0], _fio[1], _fio[2]), avg, group);
                add();
            }
        }

        private void comboboxSql_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboboxSql.SelectedIndex)
            {
                case 0:
                    labelHowMuch.Content = "Кол-во учеников: " + sqlDoc.MoreThanFour();
                    break;
                case 1:
                    labelHowMuch.Content = "Кол-во учеников: " + sqlDoc.SmallerThanFour();
                    break;
                case 2:
                    labelHowMuch.Content = "Кол-во учеников: " + sqlDoc.Count();
                    break;
                case 3:
                    labelHowMuch.Content = "Кол-во учеников: " + sqlDoc.GoodMoney();
                    break;
                case 4:
                    Grid.ItemsSource = sqlDoc.Sort();
                    break;
                case 5:
                    Grid.ItemsSource = sqlDoc.GoodMarkups();
                    break;
                default:
                    break;
            }
        }
    }
}
    public struct Student
    {
        private string _surname = "Undef",_name = "Undef", _patronymic = "";
        
        public string Surname { get { return _surname; } set { _surname = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Patronymic { get { return _patronymic; } set { _patronymic = value; } }

        public Student(string surname, string name, string patronymic)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }
    }
    public struct Lesson
    {
        private string _name = "undef";
        public string Name { get { return _name; } set { _name = value; } }
        private int _point = 4;
        public int Point { get { return _point; } set { _point = value; } }

        public Lesson(string name, int point)
        {
            Name = name;
            Point = point;
        }
        
    }
    public struct Group
{
    public string? Title { get; set; }
    public Group()
    {
        Random rand = new Random();
        Title = "";
        for (int i = 0; i < 3; i++)
        {
            Title += (char)rand.Next('A', 'Z' + 1);
        }

        Title += "-";
        Title += rand.Next(10,30).ToString();
    }
}
