using MobileCaller.ComPort;
using MobileCaller.Enums;

namespace MobileCaller.Utils
{
    static class ComPortHelper
    {
        /// <summary>
        /// Convert CPAS specific format of command response to user response code.
        /// </summary>
        /// <param name="status">Specific state of call for CPAS response.</param>
        /// <returns>User response code.</returns>
        /// <remarks>Returns CONNECT, RING and DEFAULT codes.</remarks>
        public static ResponseCode ConvertCPASToResponseCode(int status)
        {
            switch (status)
            {
                case 0:
                    return ResponseCode.DEFAULT; // ready to work (command are available from TA/TE)
                case 1:
                    return ResponseCode.DEFAULT; // unavailable (commands are not available)
                case 2:
                    return ResponseCode.DEFAULT; // unknown (ME is not guaranteed to respond to instructions).
                case 3:
                    return ResponseCode.RING; // Incoming call (phone is ringing)
                case 4:
                    return ResponseCode.CONNECT; // call in progress(выполняется исходящий вызов) or call hold
                case 5:
                    return ResponseCode.DEFAULT; // sleep mode
            }
            return ResponseCode.DEFAULT;
        }

        /// <summary>
        /// Convert CLCC specific format of command response to user response code.
        /// </summary>
        /// <param name="status">Specific state of call for CLCC response.</param>
        /// <param name="direction">
        /// Specific direction of call. 0 - outgoing or MO(mobile originated).
        ///                             1 - incoming or MT(mobile terminated).</param>
        /// <param name="cause">Формат вывода ^CEND:call_index, duration, end_status, cc_cause
        /// где:
        /// call_index — уникальный идентификатор вызова
        /// duration — длительность вызова в секундах
        /// end_status — код статуса устройства после завершения вызова
        /// cc_cause — код причины завершения вызова
        /// </param>
        /// <returns>User response code.</returns>
        /// <remarks>Returns CONNECT, BUSY, RING and DEFAULT codes.</remarks>
        public static ResponseCode ConvertCLCCToResponseCode(int direction, int status, int? cause)
        {
            if (cause.HasValue && cause.Value == 17 /*'USER_BUSY'*/)
            {
                // Абонент сбросил вызов.
                return ResponseCode.BUSY;
            }
            // Somehow for 1550 direction is 1 when the call is for +7.
            //if (direction == 0)
            {
                switch (status)
                {
                    case 0:
                        return ResponseCode.CONNECT; // response on incoming call(dir=1) or status of active outgoing call(dir=0) / Соединение активно
                    case 1:
                        return ResponseCode.DEFAULT; // hold / Соединение удерживается
                    case 2:
                        return ResponseCode.DEFAULT; // dialing / MO (connection is not set yet) / Осуществляется набор (в режиме набора)
                    case 3:
                        return ResponseCode.RING; // alerting / MO (connection is set) / Вызывается абонент
                    case 4:
                        return ResponseCode.DEFAULT; // incoming call / MT
                    case 5:
                        return ResponseCode.DEFAULT; // waiting / MT
                }
            }
            return ResponseCode.DEFAULT;
        }

        /// <summary>
        /// Convert CEND specific format of command response to user response code.
        /// </summary>
        /// <param name="cause">Формат вывода ^CEND:call_index, duration, end_status, cc_cause
        /// где:
        /// call_index — уникальный идентификатор вызова
        /// duration — длительность вызова в секундах
        /// end_status — код статуса устройства после завершения вызова
        /// cc_cause — код причины завершения вызова
        /// </param>
        /// <returns>User response code.</returns>
        public static ResponseCode ConvertCENDResponseCode(int cause)
        {
            if (cause == 16 /*'NORMAL_CALL_CLEARING'*/)
            {
                // Либо в результате нашего события "повесить трубку", либо абонент не активирован
                return ResponseCode.OK;
            }
            if (cause == 17 /*'USER_BUSY'*/)
            {
                // Абонент сбросил вызов.
                return ResponseCode.BUSY;
            }
            return ResponseCode.DEFAULT;
        }

        /* коды disconnect cause (cc)
# English http://www.eversoft.net/dcc.html
# по Русски http://ru.wikipedia.org/wiki/Q.931
# маны по huawei
# HUAWEI CDMA Datacard Modem AT Command Interface Specification
# "http://www.letswireless.com.cn/asp_bin/downfile/2009929121443234.pdf"
#
# HUAWEI UMTS Datacard Modem AT Command Interface Specification
# "http://www.net139.com/UploadFile/menu/HUAWEI%20UMTS%20Datacard%20Modem%20AT%20Command%20Interface%20Specification_V2.3.pdf"
(
'1' => 'UNASSIGNED_CAUSE',
'3' => 'NO_ROUTE_TO_DEST',
'6' => 'CHANNEL_UNACCEPTABLE',
'8' => 'OPERATOR_DETERMINED_BARRING',
'16' => 'NORMAL_CALL_CLEARING',
'17' => 'USER_BUSY',
'18' => 'NO_USER_RESPONDING',
'19' => 'USER_ALERTING_NO_ANSWER',
'21' => 'CALL_REJECTED',
'22' => 'NUMBER_CHANGED',
'26' => 'NON_SELECTED_USER_CLEARING',
'27' => 'DESTINATION_OUT_OF_ORDER',
'28' => 'INVALID_NUMBER_FORMAT',
'29' => 'FACILITY_REJECTED',
'30' => 'RESPONSE_TO_STATUS_ENQUIRY',
'31' => 'NORMAL_UNSPECIFIED',
'34' => 'NO_CIRCUIT_CHANNEL_AVAILABLE',
'38' => 'NETWORK_OUT_OF_ORDER',
'41' => 'TEMPORARY_FAILURE',
'42' => 'SWITCHING_EQUIPMENT_CONGESTION',
'43' => 'ACCESS_INFORMATION_DISCARDED',
'44' => 'REQUESTED_CIRCUIT_CHANNEL_NOT_AVAILABLE',
'47' => 'RESOURCES_UNAVAILABLE_UNSPECIFIED',
'49' => 'QUALITY_OF_SERVICE_UNAVAILABLE',
'50' => 'REQUESTED_FACILITY_NOT_SUBSCRIBED',
'55' => 'INCOMING_CALL_BARRED_WITHIN_CUG',
'57' => 'BEARER_CAPABILITY_NOT_AUTHORISED',
'58' => 'BEARER_CAPABILITY_NOT_PRESENTLY_AVAILABLE',
'63' => 'SERVICE_OR_OPTION_NOT_AVAILABLE',
'65' => 'BEARER_SERVICE_NOT_IMPLEMENTED',
'68' => 'ACM_GEQ_ACMMAX',
'69' => 'REQUESTED_FACILITY_NOT_IMPLEMENTED',
'70' => 'ONLY_RESTRICTED_DIGITAL_INFO_BC_AVAILABLE',
'79' => 'SERVICE_OR_OPTION_NOT_IMPLEMENTED',
'81' => 'INVALID_TRANSACTION_ID_VALUE',
'87' => 'USER_NOT_MEMBER_OF_CUG',
'88' => 'INCOMPATIBLE_DESTINATION',
'91' => 'INVALID_TRANSIT_NETWORK_SELECTION',
'95' => 'SEMANTICALLY_INCORRECT_MESSAGE',
'96' => 'INVALID_MANDATORY_INFORMATION',
'97' => 'MESSAGE_TYPE_NON_EXISTENT',
'98' => 'MESSAGE_TYPE_NOT_COMPATIBLE_WITH_PROT_STATE',
'99' => 'IE_NON_EXISTENT_OR_NOT_IMPLEMENTED',
'100' => 'CONDITIONAL_IE_ERROR',
'101' => 'MESSAGE_NOT_COMPATIBLE_WITH_PROTOCOL_STATE',
'102' => 'RECOVERY_ON_TIMER_EXPIRY',
'111' => 'PROTOCOL_ERROR_UNSPECIFIED',
'127' => 'INTERWORKING_UNSPECIFIED',
'160' => 'REJ_UNSPECIFIED',
'161' => 'AS_REJ_RR_REL_IND',
'162' => 'AS_REJ_RR_RANDOM_ACCESS_FAILURE',
'163' => 'AS_REJ_RRC_REL_IND',
'164' => 'AS_REJ_RRC_CLOSE_SESSION_IND',
'165' => 'AS_REJ_RRC_OPEN_SESSION_FAILURE',
'166' => 'AS_REJ_LOW_LEVEL_FAIL',
'167' => 'AS_REJ_LOW_LEVEL_FAIL_REDIAL_NOT_ALLOWD',
'168' => 'MM_REJ_INVALID_SIM',
'169' => 'MM_REJ_NO_SERVICE',
'170' => 'MM_REJ_TIMER_T3230_EXP',
'171' => 'MM_REJ_NO_CELL_AVAILABLE',
'172' => 'MM_REJ_WRONG_STATE',
'173' => 'MM_REJ_ACCESS_CLASS_BLOCKED',
'174' => 'ABORT_MSG_RECEIVED',
'175' => 'OTHER_CAUSE',
'176' => 'CNM_REJ_TIMER_T303_EXP',
'177' => 'CNM_REJ_NO_RESOURCES',
'178' => 'CNM_MM_REL_PENDING',
'179' => 'CNM_INVALID_USER_DATA'
);*/

        /*
         # коды Call ending cause codes
# маны по huawei
#
# HUAWEI CDMA Datacard Modem AT Command Interface Specification
# "http://www.letswireless.com.cn/asp_bin/downfile/2009929121443234.pdf"
#
# HUAWEI UMTS Datacard Modem AT Command Interface Specification
# "http://www.net139.com/UploadFile/menu/HUAWEI%20UMTS%20Datacard%20Modem%20AT%20Command%20Interface%20Specification_V2.3.pdf"
(
'0' => 'The board is offline.',
'21' => 'Board is out of service.',
'22' => 'Call is ended normally.',
'23' => 'Call is interrupted by BS.',
'24' => 'BS record is received during a call.',
'25' => 'BS releases a call.',
'26' => 'BS rejects the current SO service.',
'27' => 'There is incoming BS call.',
'28' => 'received alert stop from BS.',
'29' => 'Call is ended normally by the client end.',
'30' => 'received end activation — OTASP call.',
'31' => 'MC ends call initiatin oor call.',
'34' => 'RUIM is not available.',
'99' => 'NDSS error.',
'100' => 'rxd a reason from lower layer,look in cc_cause',
'101' => 'After a MS initiates a call, the network fails to respond.',
'102' => 'MS rejects an incoming call.',
'103' => 'A call is rejected during the put-through process.',
'104' => 'The release is from the For details, check',
'105' => 'The phone fee is used up.',
'106' => 'The MS is out of the service'
);
         */
    }
}
