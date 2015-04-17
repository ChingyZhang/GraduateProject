function LeftCol_OnLoad(id)
{
    var alwaysshow = Get_Cookie('AlwaysShowLeftCol');
        
    if(alwaysshow!='false')
    {
        showLeftCol(id);
    }
    else
    {
        HideLeftCol(id);
    }
}

function showLeft_Click(id)
{
    var alwaysshow = Get_Cookie('AlwaysShowLeftCol');
    if (alwaysshow!='true')
    {
        Set_Cookie('AlwaysShowLeftCol','true',30,'/','','');
        showLeftCol(id);
    }
    else
    {
        HideLeftCol(id);
    }
}

function showLeftCol(id)
{
    document.getElementById(id).style.display='inline';
    document.forms[0]['ctl00_HideHandle'].src = document.forms[0]['ctl00_HideHandle'].src.replace('show', 'hide');
    
    var alwaysshow = Get_Cookie('AlwaysShowLeftCol');
    if (alwaysshow!='false')
        document.getElementById("td_left").innerHTML="<table width='160'><tr><td></td></tr></table>";
}

function HideLeftCol(id)
{
    Set_Cookie('AlwaysShowLeftCol','false',30,'/','','');
    document.getElementById(id).style.display='none';
    document.forms[0]['ctl00_HideHandle'].src = document.forms[0]['ctl00_HideHandle'].src.replace('hide', 'show');
    document.getElementById("td_left").innerHTML="";
    
}