using Cl.DataAccess.EF.Models;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Cl.DataAccess.EF.VM_Ultils
{
    public static class QueryExtensions
    {
        #region Get page
        /// <summary>
        /// Lấy danh sách bản ghi theo trang và sắp xếp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="strColumnName"></param>
        /// <param name="strOrder"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns> 
        private static string _owners = "TV";
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> query,
                           int pageNum, int pageSize,
                          string strColumnName,
                           string strOrder, ref int rowsCount)
        {
            string strOrderBy = "";
            if (string.IsNullOrEmpty(strColumnName))
            {
                strOrderBy = "CreatedDate DESC";
            }
            else
            {
                strOrderBy = strColumnName + " ";
                if (!string.IsNullOrEmpty(strOrder) && strOrder != "0")
                {
                    strOrderBy += "DESC";
                }
                else
                    strOrderBy += "ASC";
            }

            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;
            return query.OrderBy(strOrderBy).Skip(excludedRows).Take(pageSize);
        }

        public static IQueryable<T> GetPaged<T>(this IQueryable<T> query, IQueryable<int> queryCount,
                               int pageNum, int pageSize,
                              string strColumnName,
                               string strOrder, ref int rowsCount)
        {
            string strOrderBy = "";
            if (string.IsNullOrEmpty(strColumnName))
            {
                strOrderBy = "CreatedDate DESC";
            }
            else
            {
                strOrderBy = strColumnName + " ";
                if (!string.IsNullOrEmpty(strOrder) && strOrder != "0")
                {
                    strOrderBy += "DESC";
                }
                else
                    strOrderBy += "ASC";
            }

            if (pageSize <= 0) pageSize = 20;

            rowsCount = queryCount.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            return query.OrderBy(strOrderBy).Skip(excludedRows).Take(pageSize);
        }
        /// <summary>
        /// Lấy danh sách bản ghi theo trang và sắp xếp
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
        /// <param name="query">Quy vấn</param>
        /// <param name="pageNum">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="rowsCount">Tổng số bản ghi</param>
        /// <returns></returns>       
        public static List<T> GetPaged<T>(this IQueryable<T> query,
                           int pageNum, int pageSize, ref int rowsCount)
        {


            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            return query.Skip(excludedRows).Take(pageSize).ToList<T>();
        }


        /// <summary>
        /// Lấy danh sách bản ghi theo trang và sắp xếp
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
        /// <param name="query">Quy vấn</param>
        /// <param name="pageNum">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="rowsCount">Tổng số bản ghi</param>
        /// <returns></returns>       
        public static IQueryable<T> GetQuery<T>(this IQueryable<T> query,
                           int pageNum, int pageSize, ref int rowsCount)
        {


            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            return query.Skip(excludedRows).Take(pageSize);
        }

        public static IQueryable<T> GetPaged<T, TResult>(this IQueryable<T> query,
                            int pageNum, int pageSize,
                            Func<T, TResult> orderByProperty,
                            bool isAscendingOrder, ref int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();

            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            //if (isAscendingOrder)
            //    query = query.OrderBy(orderByProperty);
            //else
            //    query = query.OrderByDescending(orderByProperty);

            return query.Skip(excludedRows).Take(pageSize);
        }
        #endregion Get page
        public static List<LookupDataSM> GetDanhSachMutilLoookupByStringFiledData(string fieldData)
        {
            List<LookupDataSM> ListData = new List<LookupDataSM>();
            LookupDataSM objData;
            if (!string.IsNullOrEmpty(fieldData))
            {
                List<string> splitData = fieldData.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).ToList(); ;
                for (int index = 0; index < splitData.Count; index = index + 2)
                {
                    objData = new LookupDataSM();
                    int _id = 0;
                    if (index < splitData.Count)
                        int.TryParse(splitData[index], out _id);
                    objData.ID = _id;
                    if (index + 1 < splitData.Count)
                        objData.Title = splitData[index + 1];
                    ListData.Add(objData);
                }
            }
            return ListData;
        }
        /// <summary> 
        /// Truy vấn Order by
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            // DataSource control passes the sort parameter with a direction
            // if the direction is descending          
            int descIndex = propertyName.IndexOf(" DESC");
            if (descIndex >= 0)
            {
                propertyName = propertyName.Substring(0, descIndex).Trim();
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (descIndex < 0) ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        /// <summary>
        /// Kiểm tra trùng dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="arrFieldName"></param>
        /// <param name="arrFieldValue"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static IQueryable<T> CheckExitsEx<T>(this IQueryable<T> source, string[] arrFieldName, string[] arrFieldValue, string ID = "")
        {
            Expression methodCallExpression = source.Expression;
            ParameterExpression parameter = Expression.Parameter(typeof(T), "obj");
            MemberExpression property;
            LambdaExpression lambda;
            Expression left = null, right = null, exField = null, predicateBody = null;

            for (int i = 0; i < arrFieldName.Length; i++)
            {
                property = Expression.Property(parameter, arrFieldName[i]);

                left = Expression.Call(property, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                right = Expression.Constant(arrFieldValue[i], typeof(string)); // = "value"                            
                exField = Expression.Equal(left, right);
                if (i == 0)
                    predicateBody = exField;
                else
                    predicateBody = Expression.And(predicateBody, exField);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                property = Expression.Property(parameter, "ID");
                left = Expression.Call(property, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                right = Expression.Constant(ID, typeof(string)); // = "value"     
                Expression exID = Expression.NotEqual(left, right);
                predicateBody = Expression.And(predicateBody, exID);
            }
            lambda = Expression.Lambda<Func<T, bool>>(predicateBody, parameter);
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable)
                , "Where"
                , new Type[] { source.ElementType }
                , methodCallExpression
                , Expression.Quote(lambda));

            IQueryable<T> results = source.Provider.CreateQuery<T>(whereCallExpression);
            return results;
        }

        /// <summary>
        /// Hàm lấy dữ liệu theo GridRequest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IQueryable<T> GetByGridRequest<T>(this IQueryable<T> source, GridRequest request, ref int TotalRecord)
        {
            return source.GetByGridRequest<T, DefaultClass, DefaultClass, DefaultClass>(request, ref TotalRecord);
        }
        public static IQueryable<T> GetByGridRequest<T, U>(this IQueryable<T> source, GridRequest request, ref int TotalRecord)
        {
            return source.GetByGridRequest<T, U, DefaultClass, DefaultClass>(request, ref TotalRecord);
        }
        public static IQueryable<T> GetByGridRequest<T, U, V>(this IQueryable<T> source, GridRequest request, ref int TotalRecord)
        {
            return source.GetByGridRequest<T, U, V, DefaultClass>(request, ref TotalRecord);
        }
        public static MemberExpression getProperty(ParameterExpression parameter, string tencot)
        {
            if (!tencot.Equals("-1"))
                return Expression.Property(parameter, tencot);
            return null;
        }
        /// <summary>
        /// Hàm lấy dữ liệu theo GridRequest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IQueryable<T> GetByGridRequest<T, U, V, W>(this IQueryable<T> source, GridRequest request, ref int TotalRecord)
        {
            Expression methodCallExpression = source.Expression;
            LambdaExpression lambda;
            List<ParameterExpression> lstParameter = new List<ParameterExpression>();
            lstParameter.Add(Expression.Parameter(typeof(T), "objT"));
            lstParameter.Add(Expression.Parameter(typeof(U), "objU"));
            lstParameter.Add(Expression.Parameter(typeof(V), "objV"));
            lstParameter.Add(Expression.Parameter(typeof(W), "objW"));
            List<Type> lstTypeObject = new List<Type>();
            lstTypeObject.Add(typeof(T));
            lstTypeObject.Add(typeof(U));
            lstTypeObject.Add(typeof(V));
            lstTypeObject.Add(typeof(W));
            List<MethodInfo> lstMethodAny = new List<MethodInfo>();
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            #region dùng trong tìm kiếm

            if (request.filter != null && request.filter.filters != null)
            {
                List<Expression> lstExpression = new List<Expression>();
                foreach (Filter currentfilter in request.filter.filters)
                {
                    Expression temp = GetExpressionDeQuy<T, U, V, W>(currentfilter, lstParameter, lstTypeObject);
                    if (temp != null)
                        lstExpression.Add(temp);
                }
                if (lstExpression.Count > 0)
                {
                    BinaryExpression epSum;
                    if (lstExpression.Count == 1)
                        epSum = Expression.OrElse(lstExpression[0], lstExpression[0]);
                    else
                    {
                        int countExpression = 2;
                        epSum = Expression.And(lstExpression[0], lstExpression[1]);
                        while (countExpression < lstExpression.Count)
                        {
                            epSum = Expression.And(epSum, lstExpression[countExpression]);
                            countExpression++;
                        }
                    }
                    Expression predicateTong = Expression.Lambda<Func<T, bool>>(epSum, lstParameter[0]);
                    methodCallExpression = Expression.Call(typeof(Queryable), "Where",
                                            new Type[] { source.ElementType },
                                            methodCallExpression, Expression.Quote(predicateTong));
                }
            }
            source = source.Provider.CreateQuery<T>(methodCallExpression);
            #endregion

            #region dùng trong sắp xếp
            if (!(request.sort != null && request.sort.Count > 0))// || request.sort.Count == 0) //Nếu ko có order đặt = properties đầu tiên
            {
                List<Sort> lstSort = new List<Sort>();
                lstSort.Add(new Sort()
                {
                    dir = "asc",
                    field = "Title"
                });
                request.sort = lstSort;
            }
            string propertyName = request.sort[0].field;
            string methodName = (request.sort[0].dir == "asc") ? "OrderBy" : "OrderByDescending";
            string[] tmpArraySortField = propertyName.Split('.');
            ParameterExpression parameter;
            MemberExpression tmpProperty;
            if (tmpArraySortField.Length == 1)
            {
                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, propertyName);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            else
            {
                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, tmpArraySortField[0]);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            for (int iii = 1; iii < request.sort.Count; iii++)
            {
                propertyName = request.sort[iii].field;
                methodName = (request.sort[iii].dir == "asc") ? "ThenBy" : "ThenByDescending";

                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, propertyName);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            #endregion


            #region dùng trong phân trang
            TotalRecord = source.Count();

            if (request.page > 0 && request.pageSize > 0)
            {

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Skip",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant((request.page - 1) * request.pageSize));
                source = source.Provider.CreateQuery<T>(methodCallExpression);

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Take",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant(request.pageSize));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            return source;
            #endregion
        }
        static Expression GetExpressionDeQuy<T, U, V, W>(Filter currentfilter, List<ParameterExpression> lstParameter, List<Type> lstTypeObject)
        {
            Expression result = null;
            if (string.IsNullOrEmpty(currentfilter.logic))
            {
                #region So sánh trực tiếp
                List<string> lstField = currentfilter.field.Split('.').ToList();
                string strValue = currentfilter.value.Trim();
                string strPhuongThuc = currentfilter.phuongthuc;
                if (lstField.Count == 1)
                {
                    MemberExpression express = Expression.Property(lstParameter[0], currentfilter.field);
                    Type typeField = express.Type;
                    Expression temp = GetExpresionByType(typeField, express, strValue, strPhuongThuc);
                    if (temp != null)
                        result = temp;
                }
                else if (lstField.Count > 1)
                {
                    #region Trường hợp any
                    ParameterExpression parameterFieldCompare = lstParameter[lstField.Count - 1];
                    MemberExpression express = Expression.Property(parameterFieldCompare, lstField[lstField.Count - 1]);
                    Type typeField = express.Type;
                    Expression temp = GetExpresionByType(typeField, express, strValue, strPhuongThuc);
                    result = AddQueryAny<T, U, V, W>(temp, lstParameter, lstTypeObject, lstField);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Điều kiện lồng
                List<Expression> lstExpression = new List<Expression>();
                foreach (Filter childFilter in currentfilter.filters)
                {
                    Expression temp = GetExpressionDeQuy<T, U, V, W>(childFilter, lstParameter, lstTypeObject);
                    if (temp != null)
                        lstExpression.Add(temp);
                }

                if (lstExpression.Count > 0)
                {
                    BinaryExpression epSum;
                    if (currentfilter.logic == "or")
                    {
                        if (lstExpression.Count == 1)
                            epSum = Expression.OrElse(lstExpression[0], lstExpression[0]);
                        else
                        {
                            int countExpression = 2;
                            epSum = Expression.OrElse(lstExpression[0], lstExpression[1]);
                            while (countExpression < lstExpression.Count)
                            {
                                epSum = Expression.OrElse(epSum, lstExpression[countExpression]);
                                countExpression++;
                            }
                        }
                        result = epSum;
                    }
                    else
                    {
                        if (lstExpression.Count == 1)
                            epSum = Expression.And(lstExpression[0], lstExpression[0]);
                        else
                        {
                            int countExpression = 2;
                            epSum = Expression.And(lstExpression[0], lstExpression[1]);
                            while (countExpression < lstExpression.Count)
                            {
                                epSum = Expression.And(epSum, lstExpression[countExpression]);
                                countExpression++;
                            }
                        }
                        result = epSum;
                    }
                }
                #endregion
            }
            return result;
        }
        static Expression GetExpresionByType(Type typeField, MemberExpression express, string strValue, string strPhuongThuc)
        {
            if (typeField == typeof(Int64)
                                || typeField == typeof(Int32)
                                || typeField == typeof(Int64?)
                                || typeField == typeof(Int32?)
                                || typeField == typeof(double)
                                || typeField == typeof(double?)
                                || typeField == typeof(decimal)
                                || typeField == typeof(decimal?))
            {
                return AddQueryNumeric(express, strValue, strPhuongThuc);
            }
            else if (typeField == typeof(DateTime) || typeField == typeof(DateTime?))
            {
                return AddQueryDateTime(express, strValue, strPhuongThuc);
            }
            else if (typeField == typeof(string))
            {
                return AddQueryString(express, strValue, strPhuongThuc);
            }
            return null;
        }
        static MethodCallExpression AddQueryAny<T, U, V, W>(Expression expresionLast, List<ParameterExpression> lstParameter, List<Type> lstTypeObject, List<string> lstField)
        {
            MethodCallExpression result = null;
            for (int i = lstField.Count - 2; i >= 0; i--)
            {
                ParameterExpression parameterFieldCompare = lstParameter[i];
                MemberExpression express = Expression.Property(parameterFieldCompare, lstField[i]);
                MethodInfo method = typeof(Enumerable).
                                    GetMethods().
                                    Where(x => x.Name == "Any").
                                    Single(x => x.GetParameters().Length == 2).
                                    MakeGenericMethod(lstTypeObject[lstField.Count - i - 1]);
                LambdaExpression lambda = null;

                if (result == null)
                {
                    if (i == 0)
                        lambda = Expression.Lambda<Func<U, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                    else if (i == 1)
                        lambda = Expression.Lambda<Func<V, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                    if (i == 2)
                        lambda = Expression.Lambda<Func<W, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                }
                else
                {
                    if (i == 0)
                        lambda = Expression.Lambda<Func<U, bool>>(result, lstParameter[lstField.Count - i - 1]);
                    else if (i == 1)
                        lambda = Expression.Lambda<Func<V, bool>>(result, lstParameter[lstField.Count - i - 1]);
                    if (i == 2)
                        lambda = Expression.Lambda<Func<W, bool>>(result, lstParameter[lstField.Count - i - 1]);
                }
                result = Expression.Call(method, express, lambda);
            }
            return result;
        }
        static MethodCallExpression AddQueryString(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            var termConstant = Expression.Constant(strValue, typeof(string)); // = "value"
            var ToLower = Expression.Call(propertyField, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            var StartWith = Expression.Call(ToLower, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().StartsWith();
            var Contains = Expression.Call(ToLower, typeof(string).GetMethod("Contains", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().Contains();
            var Equals = Expression.Call(ToLower, typeof(string).GetMethod("Equals", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().Equals();
            var EndsWith = Expression.Call(ToLower, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().EndWith();

            if (strPhuongThuc == "contains")
                return Contains;
            else if (strPhuongThuc == "startswith")
                return StartWith;
            else if (strPhuongThuc == "endswith")
                return EndsWith;
            else if (strPhuongThuc == "lt" || strPhuongThuc == "gt")
                return null;
            else
                return Equals;
        }
        static BinaryExpression AddQueryDateTime(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            Type typeField = propertyField.Type;
            DateTime? Date_value_date = Convert.ToDateTime(strValue, dtfi);
            ConstantExpression Date_values;
            if (typeField == typeof(DateTime?))
                Date_values = Expression.Constant(Date_value_date, typeof(DateTime?));
            else
                Date_values = Expression.Constant(Date_value_date, typeof(DateTime));

            var Date_eq = Expression.Equal(propertyField, Date_values);
            var Date_neq = Expression.NotEqual(propertyField, Date_values);
            var Date_gte = Expression.GreaterThanOrEqual(propertyField, Date_values);
            var Date_gt = Expression.GreaterThan(propertyField, Date_values);
            var Date_lte = Expression.LessThanOrEqual(propertyField, Date_values);
            var Date_lt = Expression.LessThan(propertyField, Date_values);
            if (strPhuongThuc == "eq")
                return Date_eq;
            else if (strPhuongThuc == "neq")
                return Date_neq;
            else if (strPhuongThuc == "gte")
                return Date_gte;
            else if (strPhuongThuc == "gt")
                return Date_gt;
            else if (strPhuongThuc == "lte")
                return Date_lte;
            else
                return Date_lt;
        }
        static BinaryExpression AddQueryNumeric(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            ConstantExpression Int_values;
            Type typeField = propertyField.Type;
            if (typeField == typeof(double) || typeField == typeof(double?))
                Int_values = Expression.Constant(Convert.ToDouble(strValue));
            else if (typeField == typeof(decimal) || typeField == typeof(decimal?))
                Int_values = Expression.Constant(Convert.ToDecimal(strValue));
            else if (typeField == typeof(Int64))
                Int_values = Expression.Constant(Convert.ToInt64(strValue)); // = "value" + Kiểu giá trị
            else
                Int_values = Expression.Constant(Convert.ToInt32(strValue)); // = "value" + Kiểu giá trị
            var Int_eq = Expression.Equal(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_neq = Expression.NotEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_gte = Expression.GreaterThanOrEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_gt = Expression.GreaterThan(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_lte = Expression.LessThanOrEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_lt = Expression.LessThan(propertyField, Expression.Convert(Int_values, propertyField.Type));
            if (strPhuongThuc == "eq")
                return Int_eq;
            else if (strPhuongThuc == "neq")
                return Int_neq;
            else if (strPhuongThuc == "gte")
                return Int_gte;
            else if (strPhuongThuc == "gt")
                return Int_gt;
            else if (strPhuongThuc == "lte")
                return Int_lte;
            else
                return Int_lt;
        }
        private static string _owner = "MinhChau";
        private static string _prefix = "1";
        private static string _suffix = "@1410";

        /// <summary>
        /// Giải mã MD5
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <Modified>
        /// Author      Date        Comment
        /// TuanVM      15/11/2014  Tạo mới
        /// </Modified>
        private static string srKeyDecrypt(string message)
        {
            string passphrase = _owner + _suffix + DateTime.Now.Year.ToString();
            if (message.StartsWith(_prefix))
                passphrase += DateTime.Now.Month.ToString();

            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(_owners + passphrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(message.Substring(1));
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();

            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);
        }

        public static string GenerateSecurityKey(string passphrase, string publicKey)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider 
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(publicKey);
            //to convert the string to utf encoding binary 

            try
            {
                //To transform the utf binary code to md5 encrypt    
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }
            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);
        }

        private static string StringDeCode(string str)
        {
            if (string.IsNullOrEmpty(str))
                str = "";
            str = "" + str.Replace("___12", "<").Replace("___13", ">").Replace("___22", "(")
                          .Replace("___23", ")").Replace("___32", "\'").Replace("___33", "\"")
                          .Replace("___34", "*").Replace("___35", ";").Replace("___36", "%").Replace("___37", "#")
                          .Replace("___38", "&").Replace("___39", "+");
            return str.Trim();
        }
    }

    public static class OrderByHelper
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string orderBy)
        {
            return enumerable.AsQueryable().OrderBy(orderBy).AsEnumerable();
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string orderBy)
        {
            foreach (OrderByInfo orderByInfo in ParseOrderBy(orderBy))
                collection = ApplyOrderBy<T>(collection, orderByInfo);

            return collection;
        }

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> collection, OrderByInfo orderByInfo)
        {
            string[] props = orderByInfo.PropertyName.Split('.');
            Type type = typeof(T);

            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            string methodName = String.Empty;

            if (!orderByInfo.Initial && collection is IOrderedQueryable<T>)
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "ThenBy";
                else
                    methodName = "ThenByDescending";
            }
            else
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "OrderBy";
                else
                    methodName = "OrderByDescending";
            }

            //TODO: apply caching to the generic methodsinfos?
            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { collection, lambda });

        }

        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (String.IsNullOrEmpty(orderBy))
                yield break;

            string[] items = orderBy.Split(',');
            bool initial = true;
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                    throw new ArgumentException(String.Format("Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", item));

                string prop = pair[0].Trim();

                if (String.IsNullOrEmpty(prop))
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");

                SortDirection dir = SortDirection.Ascending;

                if (pair.Length == 2)
                    dir = ("desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending);

                yield return new OrderByInfo() { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }

        }

        private class OrderByInfo
        {
            public string PropertyName { get; set; }
            public SortDirection Direction { get; set; }
            public bool Initial { get; set; }
        }

        private enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
    }
    public class DefaultClass { }
}
