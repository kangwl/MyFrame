/**
 * PreLoginRequest.java
 *
 * Copyright 2012 Baidu, Inc.
 *
 * Baidu licenses this file to you under the Apache License, version 2.0
 * (the "License"); you may not use this file except in compliance with the
 * License.  You may obtain a copy of the License at:
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */
 

/**
 * @author @author@ (@author-email@)
 * 
 * @version @version@, $Date: 2012-3-22$
 * 
 */

using System.Text;

namespace Demo.Console.baidutj.api.login
{
    public class PreLoginRequest
    {


        public string osVersion { get; set; }


        public string deviceType { get; set; }


        public string clientVersion { get; set; }


        public string toString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[OS ").Append(osVersion).Append(", Type ").Append(deviceType).Append(", Ver ")
                .Append(clientVersion).Append("]");
            return builder.ToString();
        }

    }
}
