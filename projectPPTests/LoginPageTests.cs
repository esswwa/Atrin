using Microsoft.VisualStudio.TestTools.UnitTesting;
using projectPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectPP.Tests
{
    [TestClass()]
    public class LoginPageTests
    {
        [TestMethod()]
        public void Auth_login_qwerty_password_qwertyasd1_return_true()
        {
            string login = "qwerty";
            string password = "qwertyasd1";
            Assert.IsTrue(LoginPage.LoginAuth(login, password));
        }

        [TestMethod()]
        public void Auth_login_qwerty111_password_qwertyasd1111_return_false()
        {
            string login = "qwerty111";
            string password = "qwertyasd1111";
            Assert.IsFalse(LoginPage.LoginAuth(login, password));
        }

        [TestMethod()]
        public void Reg_login_qwerty_notExists_Login_return_true()
        {
            string login = "qwerty1111";
            string password = "qwertyasd1";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsTrue(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_login_qwerty_Exists_Login_return_false()
        {
            string login = "qwerty";
            string password = "qwertyasd1";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_incorrect_Login_smalllogin_return_false()
        {
            string login = "qwe";
            string password = "qwertyasd1";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_incorrect_Login_largelogin_return_false()
        {
            string login = "qwerty12gdfgfdgfdgfdgfd";
            string password = "qwertyasd";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_incorrect_password_smallpassword_return_false()
        {
            string login = "qwerty111";
            string password = "1234q";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_incorrect_password_largepassword_return_false()
        {
            string login = "qwerty111";
            string password = "1234qfdsfsdfrqwewqefsdfskuiol";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89991332532";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_correct_number_isdigit_return_true()
        {
            string login = "qwerty111";
            string password = "1234qfdgfd";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "89774551231";
            string surname = "gfgfdggfd";
            Assert.IsTrue(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }

        [TestMethod()]
        public void Reg_incorrect_number_isdigit_return_false()
        {
            string login = "qwerty111";
            string password = "1234qfdgfd";
            string name = "gfdgfd";
            string otchestvo = "fdsfdsfsd";
            string number = "hghfghfghfghfg";
            string surname = "gfgfdggfd";
            Assert.IsFalse(Profile.RegCreate(login, password, name, otchestvo, number, surname));
        }
    }
}