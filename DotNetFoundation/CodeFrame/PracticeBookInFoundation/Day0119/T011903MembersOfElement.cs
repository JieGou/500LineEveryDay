using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeBook.Day0119
{
    /*
     * 练习3 :  了解Revit api里面 Element的类的成员 。 理解值类型和引用类型存在不同的地方.
         */

    class T011904
    {
        static void Main(string[] args)
        {
            int a = 5;
            Console.WriteLine(a);
            int b = a;
            a = 10;
            Console.WriteLine("{0},{1}",a,b); //b的值不变,因为a b存在不同的地方
            Console.WriteLine("------------------------");

            // 以下都一起变,因为引用类型,指向同一个堆内存的地址
            MyClass class1 =new MyClass{A=20};
            MyClass class2 = class1;
            Console.WriteLine($"{class1.A}");
            Console.WriteLine($"{class2.A}");

            class2.A = 15;
            Console.WriteLine($"{class1.A}");
            Console.WriteLine($"{class2.A}");

            class1.A = 30;
            Console.WriteLine($"{class1.A}");
            Console.WriteLine($"{class2.A}"); 


        }
    }

  public  class MyClass
    {
        public  int A { set; get; }
    }
}

// class Element
// {
/*
    1   Element的基类: System.Object;
              接口: IDisposable;
    2   Element的派生类: 茫茫多
    3   成员:
            ArePhasesModifiable():  boolean ;如果()可以被修改,则返回true
            CanBeHidden(View):  Boolean ; 表明元素是否可以在视图中被隐藏; 如果不能被隐藏,返回false
            CanBeLockerd() : Boolean    ;表明元素是否可以被锁定
            CanDeleteSubelement(Subelement): Boolean 检查指定的子元素是否可以从元素中移除
            CanHaveAnalyticalModel():  Boolean 表明元素是否可以有 分析模型
            CanHaveTypeAssigned(): Boolean  表明元素可以 拥有类型.
            CanHaveTypeAssigned(Document,ICollection):  Boolean 表明元素们是否可以拥有类型
            ChangeTypeId(ElementId): ElementId  要分配给此元素的类型
            ChangeTypeId(Document,ICollection<ElementId,ElementId>...:
            DeleteEntity(Schema):Boolean 如果删除了则为真, 如果实体不存在,则为假.
            DeleteSubelement(Subelement):Boolean 如果目标别删除则为真,其他为假.
            DeleteSubelement(IList<Subelement>): Boolean 同上

            DisaPlayLockOnlyWhenElementIsSelected(View): Boolean 
            GetAnalyticalModel(): AnalyticalModel  返回可写的分析模型

            getBoundingBox(View):BoundingBoxXYZ 
            GetChangeTypeAny():ChangeType 可用于定义更新器触发器的ChangeType，该触发器在元素中的任何更改时触发。
            GetChangeTypeElementAddintion():ChangeType 
            GetChangeTypeElementDeletion():ChangeType
            GetChangeTypeGeometry():ChangeType
            GetChangeTypeParameter(ElementId):ChangeType
            GetChangeTypeParameter(Parameter):ChangeType

            GetDependentElements(ElementFilter):IList<ElementId> 返回逻辑子元素

            GetEntity(Schema):Entity
            GetEntitySchemaGuids():IList<Guid>

            GetExternalFileReference():ExternalFileReference    包含元素引用的外部文件的路径和类型信息的对象
            GetExternalResourceReference(ExternalResourceType):

            GetGeometryObjectFromReference(Reference):GeomeryObject

            GetMatrialArea(ElementId,Boolean):Double 

            GetMatetialIds(ElementId,Boolean):Double 

            GetMaterialVolume(ElementId):Double 获得给定Id的材料的体积

            GetMonitoredLinkelementIds(): IList<ElementId> 

            GetMonitoredLocalElementIds():IList<ElementId>

            GetOrderedParameters():IList<ElementId> 按顺序获取与元素相关的参数

            GetParameterFormatOptions(ElementId): FormatOptions

            GetParameters(String):IList<Parameter>  通过给定的名称,从元素中搜索参数

            GetPhaseStatus(ElementId):ElementOnPhaseStatus 获得输入阶段中元素的状态

            GetSubelements():IList<subelement>

            GetTypeId(): ElementId 返回元素的Id

            IsExternalReference():Boolean

            IsHidden(View):Boolean  表明元素是否永久影藏在视图中.

            IsMonitoringLinkElement():Boolean 表明元素是否监视认识链接中的任何元素

            Is MonitoringLocalElement():Boolean 表明元素是否监视任何本地元素.

            IsValidType(ElementId):Boolean 表明给定类型对元素是否有效

            LookuoParameter(string):Parameter 根据指定名称查找元素

            setElementType(ElementType,string):Void

            setEntity(Entity):Void

            //属性:

            AssemblyInstanceId:ElementId
            BoundingBox[View]: BoundingBoxXYZ
            Category:Category
            CreatedPhaseId:ElementId
            DemolishedPhaseId:ElementId
            DesignOption:DesignOption
            Document:Document
            Geometry[Options]:GeometryElement


            GroupId:ElementId
            Id: ElementId
            IsTransient:Boolean
            IsValidObject:Boolean
            LevelId:ElementId
            Location:Location
            Name:String
            OwnerViewId: ElementId
            Parameter[BuiltInParameter]:Parameter
            Parameter[Definition]:Parameter
            parameter[Guid]:Parameter
            Parameters:ParameterSet
            ParametersMap:ParameterMap

            Pinned: Boolean
            UniqueId:String
            ViewSpecific:Boolean
            WorksetId: WorksetId


int  public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
double public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
string   public sealed class String : IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
byte  public struct Byte : IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>

 */
// }