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

        public static readonly DependencyProperty LikesProperty = DependencyProperty.Register("Likes", typeof(int), typeof(Post));
        public static readonly DependencyProperty LovesProperty = DependencyProperty.Register("Loves", typeof(int), typeof(Post));
        public static readonly DependencyProperty DislikesProperty = DependencyProperty.Register("Dislikes", typeof(int), typeof(Post));
        public static readonly DependencyProperty AngrysProperty = DependencyProperty.Register("Angrys", typeof(int), typeof(Post));
        public int Likes
        {
            get { return (int)GetValue(LikesProperty); }
            set { SetValue(LikesProperty, value); }
        }

        public int Loves
        {
            get { return (int)GetValue(LovesProperty); }
            set { SetValue(LovesProperty, value); }
        }

        public int Dislikes
        {
            get { return (int)GetValue(DislikesProperty); }
            set { SetValue(DislikesProperty, value); }
        }
        public int Angrys
        {
            get { return (int)GetValue(AngrysProperty); }
            set { SetValue(AngrysProperty, value); }
        }
        
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

    }
}
