using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;

namespace EducationalQuizApp
{

    // defining the main window class for the quiz application
    public partial class MainWindow : Window
    {

        // list for storing all the questions
        private List<Question> questions = new List<Question>();
        // variable tracking the player's score
        private int score = 0;
        // variable for keeping track of the current question index
        private int currentQuestionIndex = 0;

        // constructor for initializing the main window
        public MainWindow()
        {
            InitializeComponent();
            InitializeQuestions();
        }


        // method for initializing the questions
        private void InitializeQuestions()
        {
            questions.Add(new Question("What Armenian symbol represents eternity and the universe, often depicted as a circular motif with interlacing lines?", new string[] { "Arevakhach", "Ani Cross", "Khachkar", "Vahagni knot" }, "Arevakhach"));
            questions.Add(new Question("In Armenian mythology and pagan beliefs, what is the name of the god associated with the sun and light?", new string[] { "Hayk", "Mihr", "Ara the Beautiful", "Vahagn" }, "Mihr"));
            questions.Add(new Question("What is the name of the ancient archaeological site in Armenia, often referred to as the \"Armenian Stonehenge,\" consisting of numerous standing stones arranged in a circular pattern?", new string[] { "Metsamor", "Erebuni", "Karahunj", "Teishebaini" }, "Karahunj"));
            questions.Add(new Question("In Armenian mythology, who is Astghik?", new string[] { "The goddess of love and fertility", "The god of thunder and lightning", "The guardian spirit of the mountains", "The ruler of the underworld" }, "The goddess of love and fertility"));
            questions.Add(new Question("In Armenian mythology, what mythical creature is believed to be a guardian spirit, often depicted as a dog-like creature with healing abilities?", new string[] { "Tir", "Vishap", "Two-headed lion", "Aralez" }, "Aralez"));
            questions.Add(new Question("Who directed the first Armenian feature film \"Namus\" (1926)?", new string[] { "Hamo Beknazarian", "Sergei Parajanov", "Artavazd Peleshyan", "Ruben Mamoulian" }, "Hamo Beknazarian"));
            questions.Add(new Question("In the film \"Pepo\" (1935), based on a novel by Hovhannes Tumanyan, who portrays the titular character?", new string[] { "Hrachia Nersisyan", "Hovhannes Abelian", "Avet Avetisyan", "Hrachya Harutyunyan" }, "Hrachia Nersisyan"));
            questions.Add(new Question("Which Armenian film is often considered a cinematic masterpiece for its innovative use of montage and is directed by Sergei Parajanov?", new string[] { "Sayat-Nova", "The Color of Pomegranates", "Shadows of Our Forgotten Ancestors", "Ashik Kerib" }, "The Color of Pomegranates"));
            questions.Add(new Question("In the film \"Triangle\" (1967), directed by Henrik Malyan, which renowned Armenian actor portrays the lead character, Stepan?", new string[] { "Frunzik Mkrtchyan", "Varduhi Varderesyan", "Armen Dzhigarkhanyan", "Sos Sargsyan" }, "Frunzik Mkrtchyan"));
            questions.Add(new Question("In the film \"Menq enq mer sarery\" (We and Our Mountains), directed by Henrik Malyan, what cultural symbol serves as a central motif, representing the resilience and identity of the Armenian people?", new string[] { "Mount Ararat", "Armenian Cross Stone (Khachkar)", "Pomegranate", "Apricot Tree" }, "Mount Ararat"));
            questions.Add(new Question("Who is considered the national poet of Armenia?", new string[] { "Hovhannes Tumanyan", "Paruyr Sevak", "Raffi", "Avetik Isahakyan" }, "Hovhannes Tumanyan"));
            questions.Add(new Question("What is the title of the renowned Armenian epic that narrates the heroic adventures of Sassountsi David?", new string[] { "Anoush", "The Fool", "Sasna Tsrer", "The Immortals" }, "Sasna Tsrer"));
            questions.Add(new Question("Who authored the famous Armenian novel \"The Fool\"?", new string[] { "Paruyr Sevak", "Hovhannes Tumanyan", "Raffi", "Khachatur Abovian" }, "Raffi"));
            questions.Add(new Question("Which Armenian literary figure is known for his philosophical poetry and profound reflections on Armenian identity?", new string[] { "Raffi", "Hovhannes Shiraz", "Paruyr Sevak", "Avetik Isahakyan" }, "Paruyr Sevak"));
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // hiding greeting text and start button when the quiz starts
            greetingText.Visibility = Visibility.Collapsed;
            startButton.Visibility = Visibility.Collapsed;
            await ShowNextQuestion();  //show the 1st question
        }

        // method to display the next question
        private async Task ShowNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                Question currentQuestion = questions[currentQuestionIndex];
                questionText.Text = $"Question {currentQuestionIndex + 1}: {currentQuestion.Text}";

                // shuffling the options
                Random rng = new Random();
                string[] shuffledOptions = (string[])currentQuestion.Options.Clone();
                for (int i = shuffledOptions.Length - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);
                    string temp = shuffledOptions[i];
                    shuffledOptions[i] = shuffledOptions[j];
                    shuffledOptions[j] = temp;
                }

                // displaying the shuffled options
                option1Button.Content = shuffledOptions[0];
                option2Button.Content = shuffledOptions[1];
                option3Button.Content = shuffledOptions[2];
                option4Button.Content = shuffledOptions[3];

                // reseting button backgrounds to transparent
                option1Button.Background = Brushes.Transparent;
                option2Button.Background = Brushes.Transparent;
                option3Button.Background = Brushes.Transparent;
                option4Button.Background = Brushes.Transparent;

                // making question text and options panel visible
                questionText.Visibility = Visibility.Visible;
                optionsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                // showing a message box with the quiz result when all questions are answered
                MessageBox.Show($"Quiz finished! You answered {score} questions correctly. Your score is {((score / 14.0) * 100):F2}%");
                ResetQuiz();  //reseting the quiz
            }
        }


        // event handler for option button click
        private async void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            if (selectedButton != null)
            {
                string selectedOption = selectedButton.Content.ToString();
                Question currentQuestion = questions[currentQuestionIndex];
                if (selectedOption == currentQuestion.CorrectAnswer)
                {
                    // changing button color to green for a moment, if answer was right
                    selectedButton.Background = Brushes.LightGreen;
                    await Task.Delay(1000); // waiting for 1 second
                    selectedButton.Background = Brushes.Transparent;
                    score++; // incrementing score if the answer is correct
                }
                else
                {
                    // changing button color to red for a moment, if answer was wrong
                    selectedButton.Background = Brushes.LightCoral;
                    await Task.Delay(1000); // waiting for 1 second
                    selectedButton.Background = Brushes.Transparent;
                }
                currentQuestionIndex++;
                await ShowNextQuestion();
            }
        }


        // method to reset the quiz
        private void ResetQuiz()
        {
            score = 0;
            currentQuestionIndex = 0;
            greetingText.Visibility = Visibility.Visible;
            startButton.Visibility = Visibility.Visible;
            questionText.Visibility = Visibility.Collapsed;
            optionsPanel.Visibility = Visibility.Collapsed;
        }
    }


    // defining the Question class to represent a single quiz question
    public class Question
    {

        //properties for question text, options, and correct answer
        public string Text { get; }
        public string[] Options { get; }
        public string CorrectAnswer { get; }

        // constructor to initialize question properties
        public Question(string text, string[] options, string correctAnswer)
        {
            Text = text;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }
}
