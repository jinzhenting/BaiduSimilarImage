using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ImageSearch
{
    public class Reflections
    {
        /// <summary>
        /// 文件归类器，把输入的文件位置生成归类后的新位置
        /// </summary>
        /// <param name="sort_name">归类器名称</param>
        /// <param name="file_name">需要归类的文件位置</param>
        /// <returns>归类后的新位置</returns>
        public MethodInfo Compiler(string sort_name)
        {
            CompilerParameters cp = new CompilerParameters();// 编译器参数设定
            cp.GenerateExecutable = false;// 不生成exe
            cp.GenerateInMemory = true;// 写入内存
            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cp.ReferencedAssemblies.Add("System.Text.RegularExpressions.dll");
            cp.ReferencedAssemblies.Add("System.dll");// 指定引用的DLL
            if (!File.Exists(@"Sort\" + sort_name + ".cs"))
            {
                MessageBox.Show(@"与图库关联的归类器 Sort\" + sort_name + ".cs 不存在\r\n此图库将不支持匹配查找和文件归类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
            string code = File.ReadAllText(@"Sort\" + sort_name + ".cs");// 读取CODE
            CompilerResults cr = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(cp, code);// 调用实时编译
            if (cr.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show("归类器 " + sort_name + ".cs 编译错误\r\n\r\n" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            Assembly ay = cr.CompiledAssembly;// 反射内存副本
            object classs = ay.CreateInstance(sort_name); // 寻找类并实例化
            MethodInfo function = classs.GetType().GetMethod("Sort");// 寻找函数
            return function;
            //return (string)function.Invoke(null, new object[] { file_name });// 调用函数
        }
    }
}
