using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGRSalary.Classes
{
    public class Repository<E> : IRepository<E> where E : EntityObject
    {
        //کانتکست(مدل).پراپرتی
        protected readonly ObjectContext context;

        //آبجکت ستی که ایجاد می شود .. این آبجکت ستی از همان نوعی که تی می باشد ، است
        protected readonly ObjectSet<E> objectSet;

        /// <summary>
        /// در کانتستراکتر آبجکت ست ساخته می شود و 
        /// کانتکست مقدار دهی می شود
        /// </summary>
        /// <param name="context"></param>
        public Repository(ObjectContext context)
        {
            #region Argument Validation

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            #endregion

            this.context = context;
            this.objectSet = context.CreateObjectSet<E>();

        }

        /// <summary>
        /// تمام آبجکت ها را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<E> All()
        {
            return objectSet;
        }

        /// <summary>
        /// تمام آبجکت ها با  ابجکتهای مرتبط از طریق ریلیشن های مشخص شده را بر می گرداند 
        /// </summary>
        /// <param name="includes">رشته ریلیشن ها با جداکننده , دریافت می شوند</param>
        /// <returns>ابجکتهای سلکت شده</returns>
        public virtual IQueryable<E> All(string includes)
        {
            ObjectQuery<E> value = objectSet;
            if (String.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    value = value.Include(includeProperty.Trim());
                }
            }
            return value;
        }

        /// <summary>
        /// تمام آبجکت ها با  ابجکتهای مرتبط از طریق ریلیشن های مشخص شده را با شرط برقرار شده بر می گرداند 
        /// این تابع از سرعت کمی برخوردار است اگر که آبجکت ست کوچک است از این استفاده کنید وگرنه شرط و ریلیشن ها را 
        /// در ریپوزیتوری غیر پایه همان اینتیتی بنویسید
        /// </summary>
        /// <param name="where">شرط</param>
        /// <param name="includes">روابط</param>
        /// <returns>آبجکت ها با آن شرط همراه با آبجکت های مرتبط مشخص شده</returns>
        public virtual IQueryable<E> Find(Expression<Func<E, bool>> where, string includes)
        {
            ObjectQuery<E> value = objectSet;
            if (String.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    value = value.Include(includeProperty.Trim());
                }
            }

            return value.Where(where);
        }

        /// <summary>
        /// اضافه کردن یک آبجکت
        /// </summary>
        /// <param name="entity">آبجکت</param>
        public virtual void Add(E entity)
        {
            #region Argument Validation

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            #endregion
            try
            {
                objectSet.AddObject(entity);
            }
            catch (Exception ex)
            {
                context.DetectChanges();
                throw ex;
            }

            //objectSet.AddObject(entity);

        }

        /// <summary>
        /// اضافه کردن یک آبجکت
        /// </summary>
        /// <param name="entity">آبجکت</param>
        public virtual void AddAndSave(E entity)
        {
            #region Argument Validation

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            #endregion
            try
            {
                objectSet.AddObject(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.DetectChanges();
                throw ex;
            }


        }

        /// <summary>
        /// پاک کردن آبجکت
        /// </summary>
        /// <param name="entity">آبجکت</param>
        public virtual void Remove(E entity)
        {
            #region Argument Validation

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            #endregion

            if (entity.EntityState == EntityState.Detached)
            {
                objectSet.Attach(entity);
            }
            objectSet.DeleteObject(entity);

        }

        /// <summary>
        /// این تابع در آبجکت ست ، آبجکت های منطبق با شرط را پیدا می کند
        /// </summary>
        /// <param name="where">شرط</param>
        /// <returns>آبجکت های منطبق با شرط</returns>
        public virtual IEnumerable<E> Find(Expression<Func<E, bool>> where)
        {
            return objectSet.Where<E>(where);
        }

        /// <summary>
        /// این تابع اولین آبجکت منطبق با شرط را بر می گرداند
        /// </summary>
        /// <param name="where">شرط</param>
        /// <returns>اولین آبجکت منطبق با شرط</returns>
        public virtual E First(Expression<Func<E, bool>> where)
        {
            return objectSet.First<E>(where);
        }

        /// <summary>
        /// اعمال تغییرات در دیتابیس فیزیکی
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// تابعی که کالکشن را بر اساس مقادیر ذخیره شده در دیتابیس بروز رسانی می کند
        /// </summary>
        /// <param name="collection">کالکشن</param>
        public void UpdateStoreWins(IEnumerable<E> collection)
        {
            context.Refresh(RefreshMode.StoreWins, collection);
        }

        /// <summary>
        /// تابعی که اینتیتی را بر اساس مقادیر ذخیره شده در دیتابیس بروز رسانی می کند
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateStoreWins(E entity)
        {
            context.Refresh(RefreshMode.StoreWins, entity);
        }


        public void UpdateClientWins(E entity)
        {
            context.Refresh(RefreshMode.ClientWins, entity);
        }


        public void Cancel(E entity)
        {
            //context.Refresh(RefreshMode.StoreWins, entity);
        }

        /// <summary>
        /// این تابع وجود آبجکتی با این شرط را چک می کند
        /// </summary>
        /// <param name="where">شرط</param>
        /// <returns>اگر ترو باشد یعنی آبجکتی با این شرایط وجود دارد</returns>
        public virtual bool Any(Expression<Func<E, bool>> where)
        {
            return objectSet.Any(where);
        }



        //  Loging Database Part In Test Period ...........
        public void SaveWithLog(int _userID)
        {
            //try
            //{
            //    string Values = " Values ";
            //    foreach (
            //           var Item in
            //               context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified |
            //                                                                                 EntityState.Added |
            //                                                                                 EntityState.Deleted))
            //    {
            //        Values += string.Format("('{0}','{1}','{2}','{3}','{4}'),", _userID, DateTime.Now, Item.EntitySet, XmlHelper.ObjectToXml(Item.Entity), Item.State.ToString().Substring(0, 3));
            //    }
            //    context.ExecuteStoreCommand("INSERT INTO [LOG].[RecordChangeXML]([UserID],[CreateDate],[RecordType],[RecoredBody],[Type]) " + Values.TrimEnd(','));
            //    context.SaveChanges();
            //}
            //catch (Exception)
            //{
            //}
        }
        public virtual void AddAndSaveWithLog(E entity, int _userID)
        {
            #region Argument Validation

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            #endregion

            objectSet.AddObject(entity);

            SaveWithLog(_userID);

        }

        /// <summary>
        /// اینزرت مستقیم لاگ برای مواقعی که تغییرات توسط کامند مستقیم اجرا می شوند ، بایستی در حین متودمان آبجکت را بسازیم از این متود استفاده کنیم
        /// </summary>
        /// <param name="RecordType">نام کامل جدول مورد نظر</param>
        /// <param name="_userID">نام کاربر تغییر دهنده</param>
        /// <param name="objectsXml">ایکس ام ال آماده آن آبجکت</param>
        /// <param name="Type">نوع تغییر یکی از حالات Mode,Add,Del</param>
        public virtual void InsertDirectLog(string RecordType, int _userID, List<string> objectsXml, string Type)
        {
            try
            {
                string Values = " Values ";
                foreach (var Item in objectsXml)
                {
                    Values += string.Format("('{0}','{1}','{2}','{3}','{4}'),", _userID, DateTime.Now, RecordType, Item, Type);
                }
                context.ExecuteStoreCommand("INSERT INTO [LOG].[RecordChangeXML]([UserID],[CreateDate],[RecordType],[RecoredBody],[Type]) " + Values.TrimEnd(','));
            }
            catch (Exception)
            {
            }
        }

    }

}
