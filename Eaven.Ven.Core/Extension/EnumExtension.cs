using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Eaven.Ven.Core.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 根据枚举成员获取自定义属性EnumDisplayNameAttribute的属性DisplayName
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumCustomDescription(object e)
        {
            //获取枚举的Type类型对象
            Type t = e.GetType();
            //获取枚举的所有字段
            FieldInfo[] ms = t.GetFields();

            //遍历所有枚举的所有字段
            foreach (FieldInfo f in ms)
            {
                if (f.Name != e.ToString())
                {
                    continue;
                }
                //第二个参数true表示查找EnumDisplayNameAttribute的继承链
                if (f.IsDefined(typeof(EnumDisplayNameAttribute), true))
                {
                    return
                        (f.GetCustomAttributes(typeof(EnumDisplayNameAttribute), true)[0] as EnumDisplayNameAttribute)
                            .DisplayName;
                }
            }

            //如果没有找到自定义属性，直接返回属性项的名称
            return e.ToString();
        }
        /// <summary>
        ///  把枚举的描述和值绑定到DropDownList
        /// </summary>
        /// <param name="isNameValue">是否使用枚举名做为值(value)</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetSelectList(Type enumType, string emptyKey, string emptyValue, bool isNameValue = false)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(emptyKey))
            {
                result.Add(new KeyValuePair<string, string>(emptyKey, emptyValue));
            }
            foreach (object e in Enum.GetValues(enumType))
            {
                result.Add(new KeyValuePair<string, string>(isNameValue ? e.ToString() : ((int)e).ToString(), GetDescription(e)));
            }
            return result;
        }
        /// <summary>
        ///  把枚举的描述和值绑定到DropDownList  KEY值为int类型
        /// </summary>
        /// <param name="isNameValue">是否使用枚举名做为值(value)</param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetSelectListInt(Type enumType, bool isNameValue = false)
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();

            foreach (object e in Enum.GetValues(enumType))
            {
                result.Add(new KeyValuePair<int, string>(isNameValue ? (int)e : ((int)e), GetDescription(e)));
            }
            return result;
        }
        /// <summary>
        /// 获取枚举项描述信息 
        /// </summary>
        /// <param name="en">枚举项</param>
        /// <returns></returns>
        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        /// <summary>
        /// 获取枚举项描述信息 
        /// </summary>
        /// <param name="en">枚举项</param>
        /// <returns></returns>
        public static string GetEnumDesc(Enum en, string value)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        /// <summary>
        /// 获取枚举的描述文本
        /// </summary>
        /// <param name="e">枚举成员</param>
        /// <returns></returns>
        public static string GetDescription(object e)
        {
            //获取字段信息
            System.Reflection.FieldInfo[] ms = e.GetType().GetFields();
            Type t = e.GetType();
            foreach (System.Reflection.FieldInfo f in ms)
            {
                //判断名称是否相等
                if (f.Name != e.ToString()) continue;
                //反射出自定义属性
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    //类型转换找到一个Description，用Description作为成员名称
                    DescriptionAttribute dscript = attr as DescriptionAttribute;
                    if (dscript != null)
                        return dscript.Description;
                }
            }
            //如果没有检测到合适的注释，则用默认名称
            return e.ToString();
        }
        /// <summary>
        /// 根据值得到描述文本
        /// </summary>
        public static string GetDescription(Type enumType, int? value)
        {
            if (value.HasValue)
            {
                foreach (object etype in Enum.GetValues(enumType))
                {
                    if ((int)etype == value.Value) return GetDescription(etype);
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据名字得到描述文本
        /// </summary>
        public static string GetEnumDesc(Type enumType, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (object etype in Enum.GetValues(enumType))
                {
                    if (etype.ToString() == name) return GetDescription(etype);
                }
            }
            return name;
        }
        /// <summary>
        /// 根据值得到描述文本
        /// </summary>
        public static string GetEnumDesc(Type enumType, int? value)
        {
            if (value.HasValue)
            {
                foreach (object etype in Enum.GetValues(enumType))
                {
                    if ((int)etype == value.Value) return GetDescription(etype);
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetEnumValue(Type enumType, string name)
        {
            foreach (object etype in Enum.GetValues(enumType))
            {
                if (etype.ToString() == name)
                    return ((int)etype).ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据描述文本得到值
        /// </summary>
        /// <param name="enumType">typeof()</param>
        /// <param name="description">描述文本</param>
        /// <param name="isValidityCheck">是否验证描述包含在枚举内</param>
        /// <param name="checkEmpty">是否检查空</param>
        /// <returns></returns>
        public static int? GetValue(Type enumType, string description, bool isValidityCheck = false, bool checkEmpty = false)
        {
            var topAttr = ((DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute)));
            string topDescription = (topAttr == null ? enumType.Name : topAttr.Description);

            if (string.IsNullOrWhiteSpace(description))
            {
                if (checkEmpty) throw new Exception(string.Format("{0}不能为空", topDescription));
            }
            else
            {
                foreach (object etype in Enum.GetValues(enumType))
                {
                    if (GetDescription(etype) == description || etype.ToString().Equals(description, StringComparison.CurrentCultureIgnoreCase) || ((int)etype).ToString() == description)
                        return (int)etype;
                }

                if (isValidityCheck) throw new Exception(string.Format("{0}错误[{1}]", topDescription, description));
            }
            return null;
        }
    }
    public class EnumDisplayNameAttribute : Attribute
    {
        private string _displayName;

        public EnumDisplayNameAttribute(string displayName)
        {
            this._displayName = displayName;
        }
        public string DisplayName
        {
            get { return _displayName; }
        }
    }
}
