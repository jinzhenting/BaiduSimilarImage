using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 文件归类器，
    /// </summary>
    public class Reflections
    {
        /// <summary>
        /// 把输入的文件位置生成归类后的新位置
        /// </summary>
        /// <param name="sort">归类器名称</param>
        /// <returns>归类后的新位置</returns>
        public MethodInfo Compiler(string sort)
        {
            CompilerParameters compilerParameters = new CompilerParameters();// 编译器参数设定
            compilerParameters.GenerateExecutable = false;// 不生成exe
            compilerParameters.GenerateInMemory = true;// 写入内存
            compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Text.RegularExpressions.dll");
            compilerParameters.ReferencedAssemblies.Add("System.dll");// 指定引用的DLL
            if (!File.Exists(@"Sort\" + sort + ".cs"))
            {
                MessageBox.Show(@"与图库关联的归类器 Sort\" + sort + ".cs 不存在\r\n此图库将不支持匹配查找和文件归类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
            string code = File.ReadAllText(@"Sort\" + sort + ".cs");// 读取CODE
            CompilerResults compilerResults = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(compilerParameters, code);// 调用实时编译
            if (compilerResults.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, compilerResults.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show("归类器 " + sort + ".cs 编译错误\r\n\r\n" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            Assembly assembly = compilerResults.CompiledAssembly;// 反射内存副本
            object classs = assembly.CreateInstance(sort); // 寻找类并实例化
            MethodInfo function = classs.GetType().GetMethod("Sort");// 寻找函数
            return function;
            //return (string)function.Invoke(null, new object[] { file_name });// 调用函数
        }
    }
}
