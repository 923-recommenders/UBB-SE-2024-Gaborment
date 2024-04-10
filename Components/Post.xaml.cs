using System.Windows;
using System.Windows.Controls;

namespace UBB_SE_2024_Gaborment.Components
{
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

        public int Reactions
        {
            get { return (int)GetValue(ReactionsProperty); }
            set { SetValue(ReactionsProperty, value); }
        }

        public static readonly DependencyProperty ReactionsProperty = DependencyProperty.Register("Reactions", typeof(int), typeof(Post));

        public int Comments
        {
            get { return (int)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public static readonly DependencyProperty CommentsProperty = DependencyProperty.Register("Comments", typeof(int), typeof(Post));

        public int Views
        {
            get { return (int)GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        public static readonly DependencyProperty ViewsProperty = DependencyProperty.Register("Views", typeof(int), typeof(Post));

        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }

        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(Post));

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(Post));
    }
}
