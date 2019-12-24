using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;
using Project.DataAccessor;
using Project.db;

namespace BL
{
    public class BusinessLogic
    {
        static void Main()
        {
        }

        public DataSet1 getCustomers(){
            CustomersDataAccessor DA = new CustomersDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateCustomers(DataSet1 dataSet1)
        {
            CustomersDataAccessor DA = new CustomersDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getStaff()
        {
            StaffDataAccessor DA = new StaffDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateStaff(DataSet1 dataSet1)
        {
            StaffDataAccessor DA = new StaffDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getMaterials()
        {
            MaterialsDataAccessor DA = new MaterialsDataAccessor();
            PricesDataAccessor DAprices = new PricesDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DAprices.Read(absConnection, absTransaction, dataSet1);
                DA.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateMaterials(DataSet1 dataSet1)
        {
            MaterialsDataAccessor DA = new MaterialsDataAccessor();
            PricesDataAccessor DAprices = new PricesDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DAprices.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getPricess()
        {
            PricesDataAccessor DA = new PricesDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Read(absConnection, absTransaction, dataSet1);
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updatePricess(DataSet1 dataSet1)
        {
            PricesDataAccessor DA = new PricesDataAccessor();
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DA.Update(absConnection, absTransaction, dataSet1);
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }

        public DataSet1 getServicess()
        {
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DApos.Read(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }

            return dataSet1;
        }

        public void updateServicess(DataSet1 dataSet1)
        {
            ServicesDataAccessor DApos = new ServicesDataAccessor();
            AbstractConnection absConnection = null;
            AbstractTransaction absTransaction = null;
            try
            {
                absConnection = DBFactory.createConnection();
                absConnection.open();
                absTransaction = absConnection.beginTransaction();
                DApos.Update(absConnection, absTransaction, dataSet1);
                absTransaction.commit();
            }
            catch (Exception e)
            {
                absTransaction.rollback();
            }
            finally
            {
                absConnection.close();
            }
        }
    }
}
