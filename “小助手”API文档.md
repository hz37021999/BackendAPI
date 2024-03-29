# “小助手”API文档

## SelectBasicInfoHeartRate API

### 版本

V1.0

### 描述

传入设备的ID号，从数据库中读取最近100次的**心率**信息。

### 调用URL

http://134.175.95.100:81/SelectBasicInfoHeartRate.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明           |
| -------- | -------- | ------ | ------------------ |
| 是       | DeviceID | string | 采集设备的设备号。 |

### 返回值说明

| 字段        | 类型   | 说明                                              |
| ----------- | ------ | ------------------------------------------------- |
| ID          | int    | 用于区分每次信息的号码。                          |
| HeartRate   | float  | 采集到的心率数据，单位为                          |
| GetInfoTime | string | 信息上传到服务器的时间，格式为yyyyMMdd HH:mm:ss。 |



## SelectBasicInfoSpO2 API

### 版本

V1.0

### 描述

传入设备的ID号，从数据库中读取最近100次的**血氧浓度**信息。

### 调用URL

http://134.175.95.100:81/SelectBasicInfoSpO2.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明           |
| -------- | -------- | ------ | ------------------ |
| 是       | DeviceID | string | 采集设备的设备号。 |

### 返回值说明

| 字段        | 类型   | 说明                                              |
| ----------- | ------ | ------------------------------------------------- |
| ID          | int    | 用于区分每次信息的号码。                          |
| SpO2        | float  | 采集到的血氧浓度数据，单位为                      |
| GetInfoTime | string | 信息上传到服务器的时间，格式为yyyyMMdd HH:mm:ss。 |





## SelectBasicInfoTemperature API

### 版本

V1.0

### 描述

传入设备的ID号，从数据库中读取最近100次的**体温**信息。

### 调用URL

http://134.175.95.100:81/SelectBasicInfoTemperature.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明           |
| -------- | -------- | ------ | ------------------ |
| 是       | DeviceID | string | 采集设备的设备号。 |

### 返回值说明

| 字段        | 类型   | 说明                                              |
| ----------- | ------ | ------------------------------------------------- |
| ID          | int    | 用于区分每次信息的号码。                          |
| HeartRate   | float  | 采集到的体温数据，单位为                          |
| GetInfoTime | string | 信息上传到服务器的时间，格式为yyyyMMdd HH:mm:ss。 |





## SelectEmotion API

### 版本

V1.0

### 描述

传入设备的ID号，从数据库中读取最近1次的**情绪**信息。

### 调用URL

http://134.175.95.100:81/SelectEmotion.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明           |
| -------- | -------- | ------ | ------------------ |
| 是       | DeviceID | string | 采集设备的设备号。 |

### 返回值说明

| 字段        | 类型   | 说明                                              |
| ----------- | ------ | ------------------------------------------------- |
| ID          | int    | 用于区分每次信息的号码。                          |
| anger       | float  | 根据采集到的图片计算出的“愤怒”置信度              |
| disgust     | float  | 根据采集到的图片计算出的“厌恶”置信度              |
| fear        | float  | 根据采集到的图片计算出的“恐惧”置信度              |
| happiness   | float  | 根据采集到的图片计算出的“高兴”置信度              |
| neutral     | float  | 根据采集到的图片计算出的“平静”置信度              |
| sadness     | float  | 根据采集到的图片计算出的“伤心”置信度              |
| surprise    | float  | 根据采集到的图片计算出的“惊讶”置信度              |
| facenum     | float  | 根据采集到的图片识别出的人脸数。                  |
| GetInfoTime | string | 信息上传到服务器的时间，格式为yyyyMMdd HH:mm:ss。 |



## SelectIsFallLocation API

### 版本

V1.0

### 描述

传入设备的ID号，从数据库中读取最近1次的**位置**信息和**跌倒**信息。

### 调用URL

http://134.175.95.100:81/SelectIsFallLocation.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明           |
| -------- | -------- | ------ | ------------------ |
| 是       | DeviceID | string | 采集设备的设备号。 |

### 返回值说明

| 字段        | 类型   | 说明                                              |
| ----------- | ------ | ------------------------------------------------- |
| ID          | int    | 用于区分每次信息的号码。                          |
| Longitude   | string | 采集设备所在的经度信息                            |
| Latitude    | string | 采集设备所在的纬度信息                            |
| State       | bool   | 跌倒状态位，0为正常，1为跌倒                      |
| GetInfoTime | string | 信息上传到服务器的时间，格式为yyyyMMdd HH:mm:ss。 |





## SelectUserInfo API

### 版本

V1.0

### 描述

传入查看设备的ID号，从数据库中读取对应设备号和用户名的**密码**信息。

### 调用URL

http://134.175.95.100:81/SelectUserInfo.ashx

### 调用方法

GET

### 请求参数

| 是否必选 | 参数名 | 类型   | 参数说明           |
| -------- | ------ | ------ | ------------------ |
| 是       | UserID | string | 查看设备的设备号。 |

### 返回值说明

| 字段     | 类型   | 说明               |
| -------- | ------ | ------------------ |
| UserID   | string | 查看设备的设备号。 |
| UserName | string | 查看设备的用户名。 |
| Password | string | 查看设备的密码。   |





## InsertData API

### 版本

V1.0

### 描述

传入设备采集到的体征信息，存入数据库的对应字段。

### 调用URL

http://134.175.95.100:81/InsertData.ashx

### 调用方法

POST

### 请求参数

| 是否必选 | 参数名      | 类型   | 参数说明                        |
| -------- | ----------- | ------ | ------------------------------- |
| 是       | Temperature | float  | 采集设备采集的体温信息。        |
| 是       | HeartRate   | float  | 采集设备采集的心率信息。        |
| 是       | SpO2        | float  | 采集设备采集的血氧浓度信息。    |
| 是       | Pitch       | float  | 采集设备采集的俯仰角信息。      |
| 是       | Roll        | float  | 采集设备采集的翻滚角信息。      |
| 是       | Yaw         | float  | 采集设备采集的偏航角信息。      |
| 是       | AacX        | float  | 采集设备采集的x方向加速度信息。 |
| 是       | AacY        | float  | 采集设备采集的y方向加速度信息。 |
| 是       | AacZ        | float  | 采集设备采集的z方向加速度信息。 |
| 是       | Longitude   | string | 采集设备采集的经度信息。        |
| 是       | Latitude    | string | 采集设备采集的纬度信息。        |
| 是       | Altitude    | string | 采集设备采集的海拔信息。        |
| 是       | State       | bool   | 采集设备采集计算的跌倒状态。    |

### 返回值说明

| 字段 | 类型   | 说明                                                         |
| ---- | ------ | ------------------------------------------------------------ |
| code | string | 操作的结果代码。0为操作失败，1为操作成功。                   |
| msg  | string | 操作的结果信息。若失败，则输出“操作失败”和失败原因，若成功，则输出“操作成功”。 |





## InsertPicture API

### 版本

V1.0

### 描述

传入设备采集到的二进制信息，解析为图片后识别计算情绪，并存入数据库的对应字段。

### 调用URL

http://134.175.95.100:81/InsertPicture.ashx

### 调用方法

POST

### 请求参数

| 是否必选 | 参数名  | 类型       | 参数说明                 |
| -------- | ------- | ---------- | ------------------------ |
| 是       | Picture | 二进制文件 | 采集设备采集的图片数据。 |

### 返回值说明

| 字段 | 类型   | 说明                                                         |
| ---- | ------ | ------------------------------------------------------------ |
| code | string | 操作的结果代码。0为操作失败，1为操作成功。                   |
| msg  | string | 操作的结果信息。若失败，则输出“操作失败”和失败原因，若成功，则输出“操作成功”。 |





## InsertUserInfo API

### 版本

V1.0

### 描述

传入安卓设备的用户识别号、用户名、性别和密码，并存入数据库的对应字段。注册和修改密码时使用。

### 调用URL

http://134.175.95.100:81/InsertUserInfo.ashx

### 调用方法

POST

### 请求参数

| 是否必选 | 参数名   | 类型   | 参数说明                 |
| -------- | -------- | ------ | ------------------------ |
| 是       | UserID   | string | 安卓设备的设备号。       |
| 是       | UserName | string | 安卓设备的用户名。       |
| 否       | Gender   | string | 安卓设备用户的性别。     |
| 是       | Password | string | 安卓设备用户对应的密码。 |

### 返回值说明

| 字段 | 类型   | 说明                                                         |
| ---- | ------ | ------------------------------------------------------------ |
| code | string | 操作的结果代码。0为操作失败，1为操作成功。                   |
| msg  | string | 操作的结果信息。若失败，则输出“操作失败”和失败原因，若成功，则输出“操作成功”。 |

