using System;
using System.Reflection;

namespace Orange_RestWebAPI.Model.PredefinedClasses
{
    public static class SingleResponse
    {
        public static bool ArePropertiesNotNull<T>(this T obj)
        {
            bool b = false;
            var prop = typeof(T).GetProperties();

            foreach (var item in prop)
            {
                var ss = item.GetValue(typeof(T));
                b = true;
            }
            return b;
        }

        public class MyClass_Single<T> where T : new()
        {
            //public bool Status { get; set; }
            public int StatusCode { get; set; }
            public string msg { get; set; }

            //  public T data { get; set; }
            //  public List<T> datalist { get; set; }
            //public List<dynamic> data1 { get; set; }
            //public List<object> data2 { get; set; }
            public dynamic data { get; set; }
            public MyClass_Single()
            {
                StatusCode = 200;
                msg = "Success";
            }
            public T GetObject()
            {
                return new T();
            }
            public T GetObject(params object[] args)
            {
                return (T)Activator.CreateInstance(typeof(T), args);
            }

            //public Image LoadImage(string img)
            //{
            //    //data:image/gif;base64,
            //    //this image is a single pixel (black)
            //    byte[] bytes = Convert.FromBase64String(img);

            //    Image image;
            //    using (MemoryStream ms = new MemoryStream(bytes))
            //    {
            //        image = Image.FromStream(ms);
            //    }

            //    return image;
            //}

            public void initialize(dynamic state)
            {

                foreach (var prop in state.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var ssss = prop.GetValue(state, null);
                    var propname = prop.Name;
                    var type = prop.PropertyType;
                    if (ssss == null)
                    {
                        if (type == typeof(string))
                        {
                            prop.SetValue(state, "", null);
                        }
                        else if (type == typeof(Nullable<System.Int64>) || type == typeof(Nullable<Int32>) || type == typeof(Nullable<Int16>))
                        {
                            object value = System.Convert.ChangeType(0,
                 Nullable.GetUnderlyingType(prop.PropertyType));
                            prop.SetValue(state, value, null);
                        }
                        else if (type == typeof(Nullable<DateTime>))
                        {
                            prop.SetValue(state, DateTime.Now, null);
                        }
                        else if (type == typeof(Boolean))
                        {
                            prop.SetValue(state, true, null);
                        }

                    }
                }
            }

        }


        public class MyClassToken_Token<T> where T : new()
        {
            //public bool Status { get; set; }
            public int StatusCode { get; set; }
            public string msg { get; set; }
            
            public dynamic data { get; set; }
           

            public MyClassToken_Token()
            {
                StatusCode = 200;
                //Status = true;
                msg = "Success";
               
            }
            public T GetObject()
            {
                return new T();
            }
            public T GetObject(params object[] args)
            {
                return (T)Activator.CreateInstance(typeof(T), args);
            }
        }

        public class MyClassMutipleData<T> where T : new()
        {
            //public bool Status { get; set; }
            public int StatusCode { get; set; }
            public string msg { get; set; }
            
            public dynamic data { get; set; }

           

            public MyClassMutipleData()
            {
                StatusCode = 200;
                //Status = true;
                msg = "Success";
               
            }
            public T GetObject()
            {
                return new T();
            }
            public T GetObject(params object[] args)
            {
                return (T)Activator.CreateInstance(typeof(T), args);
            }
        }

    }
}
