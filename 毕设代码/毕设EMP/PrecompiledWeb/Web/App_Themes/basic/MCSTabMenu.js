function MCSTab_showMoreMenu(moremenu,contain)
{
    obj = document.getElementById(contain).getElementsByTagName("A")[0];
    objLeft   = obj.offsetLeft;
    objTop    = obj.offsetTop;
    objParent = obj.offsetParent;
    while (objParent.tagName.toUpperCase() != "BODY")
    {
        objLeft  += objParent.offsetLeft;
        objTop   += objParent.offsetTop;
        objParent = objParent.offsetParent;
    }
    objTop = objTop+5;
    document.getElementById(moremenu).style.visibility = 'visible';
    document.getElementById(moremenu).style.display = 'inline';
    document.getElementById(moremenu).style.left = objLeft;
    document.getElementById(moremenu).style.top=objTop; 
}


function MCSTab_HideMoreMenu(moremenu)
{
    document.getElementById(moremenu).style.visibility = 'hidden';
    document.getElementById(moremenu).style.display = 'none';
}
