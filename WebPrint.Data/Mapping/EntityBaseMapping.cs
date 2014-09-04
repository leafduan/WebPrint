using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{

    /// <summary>
    /// <para>NHibernate的Convetions不能很好的确定表名,故使用此基类配置主键</para>
    /// <para>方便一致修改所有类主键的生成方式(如迁移数据库时)</para>
    /// </summary>
    public class EntityBaseMapping<TEntity> : ClassMapping<TEntity> where TEntity : EntityBase
    {
        protected EntityBaseMapping(string tableName, string primayKeyName = "id")
        {
            Table(tableName);

            Id(x => x.Id, m =>
            {
                m.Column(primayKeyName);
                m.Generator(Generators.Sequence,
                    gm => gm.Params(new {sequence = string.Format("{0}_id_seq", tableName)}));
            });

            /*
            Id(x => x.Id, m =>
            {
                m.Column(primayKeyName);
                m.Generator(Generators.Identity);
            });
             * */
        }
    }
}
