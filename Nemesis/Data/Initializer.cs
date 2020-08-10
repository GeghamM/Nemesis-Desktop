using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Nemesis
{
    public class Initializer
    {
        public static void InitializeTaskTypes(ref ComboBox comboBox)
        {
            comboBox.Items.Add(new ComboBoxItem { Content = "Պլանային (Նշ. ցանկից)" });
            comboBox.Items.Add(new ComboBoxItem { Content = "ՈՒղղանկյուն կոորդինատներով" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Բևեռային եղանակով" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Զուգորդված դիտարկում" });
            comboBox.Items.Add(new ComboBoxItem { Content = "ԱԱԿ" });
            comboBox.Items.Add(new ComboBoxItem { Content = "ՇԱԿ" });

            comboBox.SelectedItem = comboBox.Items[0];

        }

        public static void InitializeSystems(ref ComboBox comboBox)
        {
            comboBox.Items.Add(new ComboBoxItem { Content = "122մմ Դ - 30" });
            comboBox.Items.Add(new ComboBoxItem { Content = "152մմ Դ - 20" });
            comboBox.Items.Add(new ComboBoxItem { Content = "152մմ Դ - 1" });
            comboBox.SelectedItem = comboBox.Items[0];
        }
        public static void InitializeShootingMethod(ref ComboBox comboBox, bool isOnlyDirect)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem { Content = "Գետնամերձ" });
            if (!isOnlyDirect)
                comboBox.Items.Add(new ComboBoxItem { Content = "Մարտիրային" });

            comboBox.SelectedItem = comboBox.Items[0];
        }

        public static void InitializeTexamas(ref ComboBox combobox)
        {
            combobox.Items.Clear();
            combobox.Items.Add(new ComboBoxItem { Content = "Առաջին" });
            combobox.Items.Add(new ComboBoxItem { Content = "Եկրորդ" });
            combobox.SelectedItem = combobox.Items[0];
        }

        public static void InitializeAXKType(ref ComboBox combobox)
        {
            combobox.Items.Clear();
            combobox.Items.Add(new ComboBoxItem { Content = "Բևեռային" });
            combobox.Items.Add(new ComboBoxItem { Content = "ՈՒղղանկյուն" });
            combobox.SelectedItem = combobox.Items[0];
        }

        public static void InitializeShootingCore(ref ComboBox comboBox, string paytucich)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem { Content = "Ավտոմատ լիցք" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք լրիվ" });
            if (paytucich != "РГМ-2 (ОФ-540)") comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք քչացված" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 1" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 2" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 3" });
            if (paytucich != "Т-7 (С-463)")
            {
                comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 4" });
                if (paytucich == "РГМ-2 (ОФ-540)")
                {
                    comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 5" });
                    comboBox.Items.Add(new ComboBoxItem { Content = "Լիցք 6" });
                }
            }
            comboBox.SelectedItem = comboBox.Items[0];
        }

        public static void InitializePositions(ref ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem { Content = "Դասակի Կազմով" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Մարտկոցի Կազմով" });

            comboBox.SelectedItem = comboBox.Items[0];
        }

        public static void InitializeDitakets(ref ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem { Content = "ՀԴՆ Հիմնական" });
            comboBox.Items.Add(new ComboBoxItem { Content = "1 ՀՄ Հիմնական" });
            comboBox.Items.Add(new ComboBoxItem { Content = "2 ՀՄ Հիմնական" });
            comboBox.Items.Add(new ComboBoxItem { Content = "3 ՀՄ Հիմնական" });
            comboBox.Items.Add(new ComboBoxItem { Content = "ՀԴՆ Պահեստային" });
            comboBox.Items.Add(new ComboBoxItem { Content = "1 ՀՄ Պահեստային" });
            comboBox.Items.Add(new ComboBoxItem { Content = "2 ՀՄ Պահեստային" });
            comboBox.Items.Add(new ComboBoxItem { Content = "3 ՀՄ Պահեստային" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Տեսախցիկ 1" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Տեսախցիկ 2" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Տեսախցիկ 3" });
            comboBox.Items.Add(new ComboBoxItem { Content = "Տեսախցիկ 4" });
            comboBox.SelectedItem = comboBox.Items[0];
        }

        public static void InitializePaytucich(ref ComboBox comboBox, string system)
        {
            comboBox.Items.Clear();
            if (system == "122մմ Դ - 30")
            {
                comboBox.Items.Add(new ComboBoxItem { Content = "РГМ-2 (ОФ-462)" });
                comboBox.Items.Add(new ComboBoxItem { Content = "В-90 (ОФ-462)" });
                comboBox.Items.Add(new ComboBoxItem { Content = "Т-90 (3С4)" });
                comboBox.Items.Add(new ComboBoxItem { Content = "Т-7 (С-463)" });
            }
            else if (system == "152մմ Դ - 20")
            {
                comboBox.Items.Add(new ComboBoxItem { Content = "РГМ-2 (ОФ-540)" });
            }
            else if (system == "152մմ Դ - 1")
            {
                comboBox.Items.Add(new ComboBoxItem { Content = "РГМ-2 (Д-1)" });
            }

                comboBox.SelectedItem = comboBox.Items[0];
        }

    }
}
