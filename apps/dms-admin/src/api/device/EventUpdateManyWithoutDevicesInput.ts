import { EventWhereUniqueInput } from "../event/EventWhereUniqueInput";

export type EventUpdateManyWithoutDevicesInput = {
  connect?: Array<EventWhereUniqueInput>;
  disconnect?: Array<EventWhereUniqueInput>;
  set?: Array<EventWhereUniqueInput>;
};
