@IF NOT EXIST m:\ (
@ECHO δ����m��
@PAUSE
@EXIT
)

@SET CSC20=c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
@echo Ŀǰ���Բ��� -ch ��������, -en Ӣ��, -ct��������, -ko ����
@echo ϵͳ׼������������Ϣ

set /p language=���������Բ���

DEL code\*.* /Q
DEL protodata\*.* /Q

proto_conv\bin\Release\proto_conv.exe %language% �ؿ���.xls



@pause
DEL m:\Assets\Lib\ClientProto\*.* /Q
DEL m:\Assets\Resources\Config\ClientProto\*.* /Q
@FOR %%P IN (code\*) DO xcopy %%P m:\Assets\Lib\ClientProto\ /Y /Q
@FOR %%P IN (protodata\*) DO xcopy %%P m:\Assets\Resources\Config\ClientProto\ /Y /Q
	
@pause
