namespace MobileCaller.Enums
{
    public enum ResponseCode
    {
        CONNECT,
        BUSY, // абонент отклонил звонок
        NO_ANSWER, // Когда линия разрывается после того, как пришел код RING.
        NO_CARRIER, // Когда линия разрывается до того, как пришел код RING.
        TIMEOUT,
        OK,
        SMS_TEXT,
        SMS,
        ERROR, // temporary fault of modem answer on AT command. It is not important and can be ignored during call but not in notifications.
        RING, // Когда набор номера отработал на станции и она подключает вас к абоненту. 
              // Т.е. после этого кода в трубке слышны либо гудки, либо атоотвечик, либо оператор.
        BLACKLISTED,
        NO_ANSWER_MODEM, // modem is not responding on commands
        DEFAULT
    }
}
