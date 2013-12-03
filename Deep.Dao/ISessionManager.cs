#region << Copyright & License >>
/*
 * ========================================================================
 * Copyright(c) 2008-2012 The ACO Co.,Ltd. All Rights Reserved.
 * ========================================================================
 * Licensed under the ACO end user License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at 
 * http://www.acortinfo.com/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 * ========================================================================
*/
#endregion

#region File Description
/*
 * -----------------------------------------------------------------------
 * File Name : ISessionManager
 * CLR Version : 4.0.30319.225 
 * Create At : MATRIXSRV    2012/10/20 13:48:20 
 * Author : root   hodoinfo@gmail.com
 * Description :   
 *  
 * ----------------------------------------------------------------------- 
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate;

namespace deep.dao
{
    interface ISessionManager
    {
        IDbConnection GetConnection();               
        void CloseConnection();

        void BeginTransaction();
        void CommitTransaction();        
        void RollbackTransaction();
       
        ISession openSession();
        
    }
}
