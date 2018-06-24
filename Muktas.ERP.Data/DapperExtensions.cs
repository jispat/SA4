using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{
    internal static class DapperExtensions
    {
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param, string PKName)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param, PKName), param);
            return result.First();
        }

        public static void Update(this IDbConnection cnn, string tableName, dynamic param, string PKName)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param, PKName), param);
        }
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param, string PKName,IDbTransaction tran)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param, PKName), param,tran);
            return result.First();
        }

        public static void Update(this IDbConnection cnn, string tableName, dynamic param, string PKName, IDbTransaction tran)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param, PKName), param,tran);
        }
    }

    internal sealed class DynamicQuery
    {
        /// <summary>
        /// Gets the insert query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The Sql query based on the item properties.
        /// </returns>
        public static string GetInsertQuery(string tableName, dynamic item, string PKName)
        {
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.Where(s => s.Name != PKName).Where(x => x.Name != "CreatedOn").Where(x => x.Name != "UpdatedOn").Where(x => x.PropertyType.BaseType != null && x.PropertyType.BaseType.Name != "BaseModel").Where(x => x.PropertyType.Name != "List`1").Where(x => x.PropertyType.Name != "IEnumerable`1").Select(p => p.Name).ToArray();
            //string[] columns = props.Select(p => p.Name).Where(s => s != PKName).ToArray();
            var query = string.Format("INSERT INTO {0} ({1}) OUTPUT inserted.{2} VALUES (@{3})",
                                 tableName,
                                 string.Join(",", columns),
                                 PKName,
                                 string.Join(",@", columns));
            return query;
        }

        /// <summary>
        /// Gets the update query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The Sql query based on the item properties.
        /// </returns>
        public static string GetUpdateQuery(string tableName, dynamic item, string PKName)
        {
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.Where(p => p.Name != PKName).Where(x => x.Name != "CreatedOn").Where(x => x.PropertyType.BaseType != null && x.PropertyType.BaseType.Name != "BaseModel").Where(x => x.PropertyType.Name != "List`1").Where(x => x.PropertyType.Name != "IEnumerable`1").Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            var query = string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", tableName, string.Join(",", parameters), PKName).Replace("@UpdatedOn", "GetDate()");
            return query;
        }

        /// <summary>
        /// Gets the dynamic query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>A result object with the generated sql and dynamic params.</returns>
        //public static QueryResult GetDynamicQuery<T>(string tableName, Expression<Func<T, bool>> expression)
        //{
        //    var queryProperties = new List<QueryParameter>();
        //    var body = (BinaryExpression)expression.Body;
        //    IDictionary<string, Object> expando = new ExpandoObject();
        //    var builder = new StringBuilder();

        //    // walk the tree and build up a list of query parameter objects
        //    // from the left and right branches of the expression tree
        //    WalkTree(body, ExpressionType.Default, ref queryProperties);

        //    // convert the query parms into a SQL string and dynamic property object
        //    builder.Append("SELECT * FROM ");
        //    builder.Append(tableName);
        //    builder.Append(" WHERE ");

        //    for (int i = 0; i < queryProperties.Count(); i++)
        //    {
        //        QueryParameter item = queryProperties[i];

        //        if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
        //        {
        //            builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
        //                                         item.QueryOperator));
        //        }
        //        else
        //        {
        //            builder.Append(string.Format("{0} {1} @{0} ", item.PropertyName, item.QueryOperator));
        //        }

        //        expando[item.PropertyName] = item.PropertyValue;
        //    }

        //    return new QueryResult(builder.ToString().TrimEnd(), expando);
        //}

        //    /// <summary>
        //    /// Walks the tree.
        //    /// </summary>
        //    /// <param name="body">The body.</param>
        //    /// <param name="linkingType">Type of the linking.</param>
        //    /// <param name="queryProperties">The query properties.</param>
        //    private static void WalkTree(BinaryExpression body, ExpressionType linkingType,
        //                                 ref List<QueryParameter> queryProperties)
        //    {
        //        if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
        //        {
        //            string propertyName = GetPropertyName(body);
        //            dynamic propertyValue = body.Right;
        //            string opr = GetOperator(body.NodeType);
        //            string link = GetOperator(linkingType);

        //            queryProperties.Add(new QueryParameter(link, propertyName, propertyValue.Value, opr));
        //        }
        //        else
        //        {
        //            WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
        //            WalkTree((BinaryExpression)body.Right, body.NodeType, ref queryProperties);
        //        }
        //    }

        //    /// <summary>
        //    /// Gets the name of the property.
        //    /// </summary>
        //    /// <param name="body">The body.</param>
        //    /// <returns>The property name for the property expression.</returns>
        //    private static string GetPropertyName(BinaryExpression body)
        //    {
        //        string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

        //        if (body.Left.NodeType == ExpressionType.Convert)
        //        {
        //            // hack to remove the trailing ) when convering.
        //            propertyName = propertyName.Replace(")", string.Empty);
        //        }

        //        return propertyName;
        //    }

        //    /// <summary>
        //    /// Gets the operator.
        //    /// </summary>
        //    /// <param name="type">The type.</param>
        //    /// <returns>
        //    /// The expression types SQL server equivalent operator.
        //    /// </returns>
        //    /// <exception cref="System.NotImplementedException"></exception>
        //    private static string GetOperator(ExpressionType type)
        //    {
        //        switch (type)
        //        {
        //            case ExpressionType.Equal:
        //                return "=";
        //            case ExpressionType.NotEqual:
        //                return "!=";
        //            case ExpressionType.LessThan:
        //                return "<";
        //            case ExpressionType.GreaterThan:
        //                return ">";
        //            case ExpressionType.AndAlso:
        //            case ExpressionType.And:
        //                return "AND";
        //            case ExpressionType.Or:
        //            case ExpressionType.OrElse:
        //                return "OR";
        //            case ExpressionType.Default:
        //                return string.Empty;
        //            default:
        //                throw new NotImplementedException();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Class that models the data structure in coverting the expression tree into SQL and Params.
        ///// </summary>
        //internal class QueryParameter
        //{
        //    public string LinkingOperator { get; set; }
        //    public string PropertyName { get; set; }
        //    public object PropertyValue { get; set; }
        //    public string QueryOperator { get; set; }

        //    /// <summary>
        //    /// Initializes a new instance of the <see cref="QueryParameter" /> class.
        //    /// </summary>
        //    /// <param name="linkingOperator">The linking operator.</param>
        //    /// <param name="propertyName">Name of the property.</param>
        //    /// <param name="propertyValue">The property value.</param>
        //    /// <param name="queryOperator">The query operator.</param>
        //    internal QueryParameter(string linkingOperator, string propertyName, object propertyValue, string queryOperator)
        //    {
        //        this.LinkingOperator = linkingOperator;
        //        this.PropertyName = propertyName;
        //        this.PropertyValue = propertyValue;
        //        this.QueryOperator = queryOperator;
        //    }
        //}

    }
}
