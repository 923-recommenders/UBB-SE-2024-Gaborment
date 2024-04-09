using System.Windows;
using System.Windows.Controls;


namespace UBB_SE_2024_Gaborment.Components
{
    /// <summary>
    /// Interaction logic for Post.xaml
    /// </summary>
    public partial class Post : UserControl
    {
        public Post()
        {
            InitializeComponent();
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(Post));

        public string Reactions
        {
            get { return (string)GetValue(ReactionsProperty); }
            set { SetValue(ReactionsProperty, value); }
        }

        public static readonly DependencyProperty ReactionsProperty = DependencyProperty.Register("Reactions", typeof(string), typeof(Post));

        public string Comments
        {
            get { return (string)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public static readonly DependencyProperty CommentsProperty = DependencyProperty.Register("Comments", typeof(string), typeof(Post));
        public string Views
        {
            get { return (string)GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        public static readonly DependencyProperty ViewsProperty = DependencyProperty.Register("Views", typeof(string), typeof(Post));

        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }

        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(Post));
    }

}
