namespace BanlineaTest.Contacts.ViewModels
{
    public class ListItem : ObservableObject
    {
        private int index;

        private string text;


        public ListItem(int index, string text)
        {
            this.Index = index;
            this.Text = text;
        }
        
        public int Index {
            get { return this.index; }

            set {
                if (this.index == value) {
                    return;
                }

                this.index = value;
                OnPropertyChanged();
            }
        }

        public string Text {
            get { return this.text; }

            set {
                if (this.text == value) {
                    return;
                }

                this.text = value;
                OnPropertyChanged();
            }
        }
    }
}