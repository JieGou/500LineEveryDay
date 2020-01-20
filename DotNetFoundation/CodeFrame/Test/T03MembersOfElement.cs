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

namespace Test
{
    /*
     * 练习2 :  了解Revit api里面 Element的类的成员 。
         */

    class T03
    {
        static void Main(string[] args)
        {
            // // 获取当前命名空间下所有方法名
            // var classes = Assembly.GetExecutingAssembly().GetTypes();
            //
            // foreach (Type item in classes)
            // {
            //     Console.WriteLine(item.Name);
            // }
        }
    }

    class Element
    {
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





         */
    }
}