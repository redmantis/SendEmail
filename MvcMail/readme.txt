要使用 Intranet 模板，您需要启用 Windows 身份验证并禁用匿名身份验证。

有关详细说明(包括关于 IIS 6.0 的说明)，请访问
http://go.microsoft.com/fwlink/?LinkID=213745

IIS 7
1. 打开 IIS 管理器，然后导航至您的网站。
2. 在“功能”视图中，双击“身份验证”。
3. 在“身份验证”页中，选择“Windows 身份验证”。 如果没有
   “Windows 身份验证”选项，则需要确认是否在服务器上安装了 Windows
   身份验证。
        要启用 Windows 身份验证，请执行以下操作：
 a) 在“控制面板”中打开“程序和功能”。
 b) 选择“打开或关闭 Windows 功能”。
 c) 导航到“Internet Information Services”|“万维网服务”|“安全性”，
    然后确保选中“Windows 身份验证”节点。
4. 在“操作”窗格中，单击“启用”以使用 Windows 身份验证。
5. 在“身份验证”页上，选择“匿名身份验证”。
6. 在“操作”窗格中，单击“禁用”以禁用匿名身份验证。

IIS Express
1. 在 Visual Studio 中，右键单击某个项目，然后选择“使用 IIS Express”。
2. 在解决方案资源管理器中，单击您的项目，将其选中。
3. 如果“属性”窗格未打开，请务必将其打开(按 F4)。
4. 在项目的“属性”窗格中，执行以下操作：
 a) 将“匿名身份验证”设置为“禁用”。
 b) 将“Windows 身份验证”设置为“启用”。

Web Platform Installer您可以使用 Microsoft Web 平台安装程序来安装 IIS Express：
    对于 Visual Studio: http://go.microsoft.com/fwlink/?LinkID=214802
    对于 Visual Web Developer: http://go.microsoft.com/fwlink/?LinkID=214800
