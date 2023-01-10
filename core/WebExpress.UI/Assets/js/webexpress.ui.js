var webexpress = webexpress || {}
webexpress.ui = {}

webexpress.ui.events = class {
    _listeners = new Map();
    
    /**
     * Constructor
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