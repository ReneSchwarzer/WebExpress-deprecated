class events {
    _listeners = new Map();
    
    /**
     * Konstruktor
     */
    constructor() {
    }
    
    /**
     * Registriert ein Eventhandler für ein Event
     * @param label Das Event-Label
     * @param callback Die Rückrufsfunktion, wenn das Event gefeuert wird
     */
    on(label, callback) {
        this._listeners.has(label) || this._listeners.set(label, []);
        this._listeners.get(label).push(callback);
    }

    /**
     * Feuert ein Event
     * @param Das Event-Label
     * @param args Die Argumente
     */
    trigger(label, ...args) {
        let res = false;

        let _trigger = (inListener, label, ...args) => {
            let listeners = inListener.get(label);
            if (listeners && listeners.length) {
                listeners.forEach((listener) => {
                    listener(...args);
                });
                res = true;
            }
        };
        _trigger(this._listeners, label, ...args);

        return res;
    }
}

/**
 * Base on https://github.com/kurtobando/simple-tags
 */
 /*
function Tags(element, id, listOfTags)
{
    let arrayOfList = listOfTags;
    let DOMParent = document.querySelector(element);
    let DOMList;
    let DOMInput;
    let hidden;
    
    function DOMCreate()
    {
        let ul = document.createElement('ul');
        let li = document.createElement('li');
        let input = document.createElement('input');
        
        hidden = document.createElement('input');
        hidden.setAttribute("type", "hidden");
        hidden.setAttribute("name", id);
        
        DOMParent.appendChild(ul);
        DOMParent.appendChild(hidden);
        DOMParent.appendChild(input);
        DOMList = DOMParent.firstElementChild;
        DOMInput = DOMParent.lastElementChild;
    }

    function DOMRender()
    {
        DOMList.innerHTML = '';
        arrayOfList.forEach
        (
            function(currentValue, index)
            {   
                let li=document.createElement('li');
                li.innerHTML = "".concat(currentValue.toLowerCase(), "<a>&times;</a>");
                li.querySelector('a').addEventListener('click', function()
                {   
                    onDelete(index);
                    
                    return false;
                });
                
                DOMList.appendChild(li);
            }
        );

        hidden.setAttribute("value", arrayOfList.map(element => element.toLowerCase()).join(';'));
    }

    function onKeyUp()
    {
        DOMInput.addEventListener('keyup', function(event)
        {
            let text = this.value;
            if(text.endsWith(',') || text.endsWith(';') || text.endsWith(' '))
            {
                let replace = text.replace(',', '').replace(';', '').replace(' ', '').toLowerCase();
                if(replace != '')
                {
                    arrayOfList.push(replace);
                }

                this.value = '';
            }

            DOMRender();
        });
    }
    
    function onDelete(id)
    {
        arrayOfList = arrayOfList.filter(function(currentValue, index)
        {
            if(index == id)
            {
                return false;
            }

            return currentValue;
        });
        
        DOMRender();
    }

    DOMCreate();
    DOMRender();
    onKeyUp();
}*/