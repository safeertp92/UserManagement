using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liwapoi.Models.Constants
{

    public static class Errors
    {
        public static string INVALID_ROLE = "Falsches Vorbild";
        public static string ROLE_NOT_FOUND = "Rolle wurde nicht gefunden";
        public static string INVALID_ROLE_ID = "RoleId ist ungültig";
        public static string DELETE_ROLE_ERROR = "Die Rolle konnte nicht gelöscht werden. Einige Benutzer sind an die aktuelle Rolle gebunden";
        public static string ATTACH_ROLE_ERROR = "Der Benutzer konnte nicht an die Rolle angehängt werden";

        public static string INVALID_GROUP = "Falsches Gruppenmodell";
        public static string GROUP_EXIST = "Eine Gruppe mit diesem Namen ist bereits vorhanden";
        public static string GROUP_NOT_FOUND = "Gruppe wurde nicht gefunden";
        public static string INVALID_GROUP_ID = "GroupId ist ungültig";
        public static string DELETE_GROUP_ERROR = "Gruppe konnte nicht gelöscht werden. Einige Benutzer oder Geräte sind der aktuellen Gruppe zugeordnet";
        public static string ATTACH_GROUP_ERROR = "Benutzer konnte nicht zur Gruppe hinzugefügt werden";
        public static string DELETE_USER_GROUP_ERROR = "Benutzer konnte nicht aus der Gruppe gelöscht werden";
        public static string ADD_DEVICE_GROUP_ERROR = "Gerät konnte nicht zur Gruppe hinzugefügt werden";
        public static string DELETE_DEVICE_GROUP_ERROR = "Gerät konnte nicht aus der Gruppe gelöscht werden";


        public static string INVALID_DEVEUI = "Falsches DevEui";
        public static string RESOLVED_DATA_NOT_FOUND = "Aufgelöste Daten für das Gerät wurden nicht gefunden";
        public static string INVALID_PAGE = "Falscher Seitenparameter";

        public static string INVALID_USER_ID = "Falsche Benutzerkennung";
        public static string DEVICE_NOT_FOUND = "Gerät nicht gefunden";
        public static string DEVICE_EXIST = "Das Gerät ist bereits vorhanden";
        public static string DEVICE_DELETED = "Das Gerät ist im System vorhanden ist, aber es wurde gelöscht. Bitte kontaktieren Sie Administrator";

        public static string INVALID_EMAIL = "E-Mail konnte nicht leer sein";
        public static string USER_NOT_FOUND = "Benutzer wurde nicht gefunden";
        public static string INVALID_TOKEN = "Token konnte nicht leer sein";
        public static string INVALID_PASSWORD = "Kennwort kann nicht leer sein";
        public static string RESTORE_ERROR = "Fehler bei der Passwort wiederherzustellen";
        public static string TOKEN_VALIDATION_ERROR = "Fehler bei der Token-Validierun.";

        public static string INVALID_USER_MODEL = "Ungültige Benutzermodell";
        public static string FILE_UPLOAD_ERROR = "Es gibt keine Dateien in Anfrage";
        public static string REGISTRATION_ERROR = "Fehler bei der Registrierung";
        public static string USER_EXIST = "Benutzer mit diesem Namen oder E-Mail ist bereits vorhanden sein";
        public static string DELETE_USER_ERROR = "Benutzer kann nicht gelöscht werden. Der Anwender ist in der Gruppe oder Vorrichtungen sind an aktuellen Benutzers angebracht";
        public static string DELETE_USER_WRONG = "Fehler beim Löschen von Benutzern";
        public static string INVALID_OLD_PASSWORD = "Altes Kennwort kann nicht leer sein";
        public static string INVALID_NEW_PASSWORD = "Neues Passwort kann nicht leer sein";
        public static string CHANGE_PASSWORD_ERROR = "Fehler bei der Benutzer-Passwort-Wechsel";
        public static string USER_EDIT_RIGHT_ERROR = "Unzureichende Bearbeitungsrechte";

        public static string INVALID_SETTINGS_MODEL = "Falsches Anpassungmodell";
        public static string SETTINGS_SAVE_ERROR = "Fehler beim Speichern der Einstellungen";
        public static string INVALID_COMMAND_MODEL = "Falsches Commandmodell";
        public static string SEND_CMD_ERROR = "Fehler beim Senden des Befehls";
        public static string INVALID_SETTINGS_INTERFACE =
            "Einstellungen konnten nicht geändert werden. Die Einstellungsoberfläche ist falsch.";

        public static string DEVICE_IS_BLOCKED = "Operationen mit dem Gerät sind nicht möglich. Gerät wird neu gestartet.";
        public static string DEVICE_FILTER_ERROR = "Gerätefiltermodell ist falsch";

        public static string CAMERA_ALREADY_EXIST = "Kamera ist bereits vorhanden";
        public static string PARKING_IS_NOT_EXIST = "Parkplätze gibt es nicht";
        public static string PARKING_DELETE_ERROR = "Fehler beim Entfernen des Parkens";

        public static string ROUTE_NOT_EXIST = "Gerät hat keine Route";

        public static string INVALID_SCHEDULER_MODEL = "Ungültige Zeitplanmodell";
        public static string INVALID_SCHEDULER_ID = "Falsches SchedulerId";

    }

}
