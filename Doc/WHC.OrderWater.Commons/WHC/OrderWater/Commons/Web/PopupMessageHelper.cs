namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Text;
    using System.Web;

    public class PopupMessageHelper
    {
        private StringBuilder stringBuilder_0 = new StringBuilder();

        public PopupMessageHelper()
        {
            this.stringBuilder_0.Append("<SCRIPT language=\"JavaScript\">  \n ");
            this.stringBuilder_0.Append("<!--  \n ");
            this.stringBuilder_0.Append("function CLASS_MSN_MESSAGE(id,width,height,caption,message,target,action){  \n ");
            this.stringBuilder_0.Append("this.id     = id;   \n ");
            this.stringBuilder_0.Append("this.caption= caption;  \n ");
            this.stringBuilder_0.Append("this.message= message;  \n ");
            this.stringBuilder_0.Append("this.target = target;  \n ");
            this.stringBuilder_0.Append("this.action = action;  \n ");
            this.stringBuilder_0.Append("this.width    = width?width:200;  \n ");
            this.stringBuilder_0.Append("this.height = height?height:120;  \n ");
            this.stringBuilder_0.Append("this.timeout= 2000;  \n ");
            this.stringBuilder_0.Append("this.speed    = 20; \n ");
            this.stringBuilder_0.Append("this.step    = 1; \n ");
            this.stringBuilder_0.Append("this.right    = screen.width -1;  \n ");
            this.stringBuilder_0.Append("this.bottom = screen.height; \n ");
            this.stringBuilder_0.Append("this.left    = this.right - this.width; \n ");
            this.stringBuilder_0.Append("this.top    = this.bottom - this.height; \n ");
            this.stringBuilder_0.Append("this.timer    = 0; \n ");
            this.stringBuilder_0.Append("this.pause    = false;\n ");
            this.stringBuilder_0.Append("this.close    = false;\n ");
            this.stringBuilder_0.Append("this.autoHide    = true;\n ");
            this.stringBuilder_0.Append("}    \n ");
            this.stringBuilder_0.Append("/*  \n ");
            this.stringBuilder_0.Append("*    隐藏消息方法  \n ");
            this.stringBuilder_0.Append("*/  \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.hide = function(){  \n ");
            this.stringBuilder_0.Append("if(this.onunload()){  \n ");
            this.stringBuilder_0.Append("var offset  = this.height>this.bottom-this.top?this.height:this.bottom-this.top; \n ");
            this.stringBuilder_0.Append("var me  = this;  \n ");
            this.stringBuilder_0.Append("if(this.timer>0){   \n ");
            this.stringBuilder_0.Append("window.clearInterval(me.timer);  \n ");
            this.stringBuilder_0.Append("}  \n ");
            this.stringBuilder_0.Append("var fun = function(){  \n ");
            this.stringBuilder_0.Append("if(me.pause==false||me.close){\n ");
            this.stringBuilder_0.Append("var x  = me.left; \n ");
            this.stringBuilder_0.Append("var y  = 0; \n ");
            this.stringBuilder_0.Append("var width = me.width; \n ");
            this.stringBuilder_0.Append("var height = 0; \n ");
            this.stringBuilder_0.Append("if(me.offset>0){ \n ");
            this.stringBuilder_0.Append("height = me.offset; \n ");
            this.stringBuilder_0.Append("}      \n ");
            this.stringBuilder_0.Append("y  = me.bottom - height;      \n ");
            this.stringBuilder_0.Append("if(y>=me.bottom){ \n ");
            this.stringBuilder_0.Append("window.clearInterval(me.timer);  \n ");
            this.stringBuilder_0.Append("me.Pop.hide();  \n ");
            this.stringBuilder_0.Append("} else { \n ");
            this.stringBuilder_0.Append("me.offset = me.offset - me.step;  \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("me.Pop.show(x,y,width,height);    \n ");
            this.stringBuilder_0.Append("}             \n ");
            this.stringBuilder_0.Append("}  \n ");
            this.stringBuilder_0.Append("this.timer = window.setInterval(fun,this.speed)      \n ");
            this.stringBuilder_0.Append("}  \n ");
            this.stringBuilder_0.Append("}    \n ");
            this.stringBuilder_0.Append("/* \n ");
            this.stringBuilder_0.Append("*    消息卸载事件，可以重写  \n ");
            this.stringBuilder_0.Append("*/  \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.onunload = function() {  \n ");
            this.stringBuilder_0.Append("return true;  \n ");
            this.stringBuilder_0.Append("}  \n ");
            this.stringBuilder_0.Append("/*  \n ");
            this.stringBuilder_0.Append("*    消息命令事件，要实现自己的连接，请重写它  \n ");
            this.stringBuilder_0.Append("*  \n ");
            this.stringBuilder_0.Append("*/  \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.oncommand = function(){  \n ");
            this.stringBuilder_0.Append("this.hide();    \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("/*  \n ");
            this.stringBuilder_0.Append("*    消息显示方法  \n ");
            this.stringBuilder_0.Append("*/  \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.show = function(){  \n ");
            this.stringBuilder_0.Append("var oPopup = window.createPopup(); /*IE5.5+ */   \n ");
            this.stringBuilder_0.Append("this.Pop = oPopup;    \n ");
            this.stringBuilder_0.Append("var w = this.width;  \n ");
            this.stringBuilder_0.Append("var h = this.height;    \n ");
            this.stringBuilder_0.Append("var str = \"<DIV style='BORDER-RIGHT: #455690 1px solid; BORDER-TOP: #a6b4cf 1px solid; Z-INDEX: 99999; LEFT: 0px; BORDER-LEFT: #a6b4cf 1px solid; WIDTH: \" + w + \"px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: \" + h + \"px; BACKGROUND-COLOR: #c9d3f3'>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TABLE style='BORDER-TOP: #ffffff 1px solid; BORDER-LEFT: #ffffff 1px solid' cellSpacing=0 cellPadding=0 width='100%' bgColor=#cfdef4 border=0>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TR>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TD style='FONT-SIZE: 12px;COLOR: #0f2c8c' width=30 height=24></TD>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TD style='PADDING-LEFT: 4px; FONT-WEIGHT: normal; FONT-SIZE: 12px; COLOR: #1f336b; PADDING-TOP: 4px' vAlign=center width='100%'>\" + this.caption + \"</TD>\"\n ");
            this.stringBuilder_0.Append(" str += \"<TD style='PADDING-RIGHT: 2px; PADDING-TOP: 2px' vAlign=center align=right width=19>\"  \n ");
            this.stringBuilder_0.Append("str += \"<SPAN title=关闭 style='FONT-WEIGHT: bold; FONT-SIZE: 12px; CURSOR: hand; COLOR: red; MARGIN-RIGHT: 4px' id='btSysClose' >\x00d7</SPAN></TD>\"  \n ");
            this.stringBuilder_0.Append("str += \"</TR>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TR>\"  \n ");
            this.stringBuilder_0.Append("str += \"<TD style='PADDING-RIGHT: 1px;PADDING-BOTTOM: 1px' colSpan=3 height=\" + (h-28) + \">\"  \n ");
            this.stringBuilder_0.Append("str += \"<DIV style='BORDER-RIGHT: #b9c9ef 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #728eb8 1px solid; PADDING-LEFT: 8px; FONT-SIZE: 12px; PADDING-BOTTOM: 8px; BORDER-LEFT: #728eb8 1px solid; WIDTH: 100%; COLOR: #1f336b; PADDING-TOP: 8px; BORDER-BOTTOM: #b9c9ef 1px solid; HEIGHT: 100%'>\" + this.message   \n ");
            this.stringBuilder_0.Append("str += \"</DIV>\"  \n ");
            this.stringBuilder_0.Append("str += \"</TD>\"  \n ");
            this.stringBuilder_0.Append("str += \"</TR>\"  \n ");
            this.stringBuilder_0.Append("str += \"</TABLE>\"  \n ");
            this.stringBuilder_0.Append("str += \"</DIV>\"    \n ");
            this.stringBuilder_0.Append("oPopup.document.body.innerHTML = str;  \n ");
            this.stringBuilder_0.Append("this.offset  = 0; \n ");
            this.stringBuilder_0.Append("var me  = this;  \n ");
            this.stringBuilder_0.Append("oPopup.document.body.onmouseover = function(){me.pause=true;}\n ");
            this.stringBuilder_0.Append("oPopup.document.body.onmouseout = function(){me.pause=false;}\n ");
            this.stringBuilder_0.Append("var fun = function(){  \n ");
            this.stringBuilder_0.Append("var x  = me.left; \n ");
            this.stringBuilder_0.Append("var y  = 0; \n ");
            this.stringBuilder_0.Append("var width    = me.width; \n ");
            this.stringBuilder_0.Append("var height    = me.height; \n ");
            this.stringBuilder_0.Append("if(me.offset>me.height){ \n ");
            this.stringBuilder_0.Append("height = me.height; \n ");
            this.stringBuilder_0.Append("} else { \n ");
            this.stringBuilder_0.Append("height = me.offset; \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("y  = me.bottom - me.offset; \n ");
            this.stringBuilder_0.Append("if(y<=me.top){ \n ");
            this.stringBuilder_0.Append("me.timeout--; \n ");
            this.stringBuilder_0.Append("if(me.timeout==0){ \n ");
            this.stringBuilder_0.Append("window.clearInterval(me.timer);  \n ");
            this.stringBuilder_0.Append("if(me.autoHide){\n ");
            this.stringBuilder_0.Append("me.hide(); \n ");
            this.stringBuilder_0.Append("}\n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("} else { \n ");
            this.stringBuilder_0.Append("me.offset = me.offset + me.step; \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("me.Pop.show(x,y,width,height);    \n ");
            this.stringBuilder_0.Append("}    \n ");
            this.stringBuilder_0.Append("this.timer = window.setInterval(fun,this.speed)        \n ");
            this.stringBuilder_0.Append("var btClose = oPopup.document.getElementById(\"btSysClose\" );    \n ");
            this.stringBuilder_0.Append("btClose.onclick = function(){  \n ");
            this.stringBuilder_0.Append("me.close = true;\n ");
            this.stringBuilder_0.Append("me.hide();  \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("}  \n ");
            this.stringBuilder_0.Append("/* \n ");
            this.stringBuilder_0.Append("** 设置速度方法 \n ");
            this.stringBuilder_0.Append("**/ \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.speed = function(s){ \n ");
            this.stringBuilder_0.Append("var t = 20; \n ");
            this.stringBuilder_0.Append("try { \n ");
            this.stringBuilder_0.Append("t = praseInt(s); \n ");
            this.stringBuilder_0.Append("} catch(e){} \n ");
            this.stringBuilder_0.Append("this.speed = t; \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("/* \n ");
            this.stringBuilder_0.Append("** 设置步长方法 \n ");
            this.stringBuilder_0.Append("**/ \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.step = function(s){ \n ");
            this.stringBuilder_0.Append("var t = 1; \n ");
            this.stringBuilder_0.Append("try { \n ");
            this.stringBuilder_0.Append("t = praseInt(s); \n ");
            this.stringBuilder_0.Append("} catch(e){} \n ");
            this.stringBuilder_0.Append("this.step = t; \n ");
            this.stringBuilder_0.Append("}   \n ");
            this.stringBuilder_0.Append("CLASS_MSN_MESSAGE.prototype.rect = function(left,right,top,bottom){ \n ");
            this.stringBuilder_0.Append("try { \n ");
            this.stringBuilder_0.Append("this.left        = left    !=null?left:this.right-this.width; \n ");
            this.stringBuilder_0.Append("this.right        = right    !=null?right:this.left +this.width; \n ");
            this.stringBuilder_0.Append("this.bottom        = bottom!=null?(bottom>screen.height?screen.height:bottom):screen.height; \n ");
            this.stringBuilder_0.Append("this.top        = top    !=null?top:this.bottom - this.height; \n ");
            this.stringBuilder_0.Append("} catch(e){} \n ");
            this.stringBuilder_0.Append("} \n ");
            this.stringBuilder_0.Append("-->  \n ");
            this.stringBuilder_0.Append("</SCRIPT> \n ");
            HttpContext.Current.Response.Write(this.stringBuilder_0.ToString());
        }

        public void Quickly(string Title, string Content)
        {
            this.Quickly(Title, Content, 0, 0);
        }

        public void Quickly(string Title, string Content, int Width, int Height)
        {
            string s = string.Format(" <script language='javascript'>var MSG1 = new CLASS_MSN_MESSAGE('DivMessage', {0}, {1}, '{2}', '{3}');MSG1.rect(null, null, null, screen.height - 50);MSG1.speed = 10;MSG1.step = 5; MSG1.show();  </script>", new object[] { Width, Height, Title, Content });
            HttpContext.Current.Response.Write(s);
        }

        public void Slowly(string Title, string Content)
        {
            this.Slowly(Title, Content, 0, 0);
        }

        public void Slowly(string Title, string Content, int Width, int Height)
        {
            string s = string.Format("<script language='javascript'>var MSG2 =new CLASS_MSN_MESSAGE('DivMessage',{0},{1},'{2}','{3}');MSG2.rect(null,null,null,screen.height); MSG2.show(); </script>", new object[] { Width, Height, Title, Content });
            HttpContext.Current.Response.Write(s);
        }

        public static PopupMessageHelper Instance
        {
            get
            {
                return new PopupMessageHelper();
            }
        }
    }
}

