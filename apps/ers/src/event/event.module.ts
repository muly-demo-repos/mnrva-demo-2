import { Module } from "@nestjs/common";
import { EventModuleBase } from "./base/event.module.base";
import { EventService } from "./event.service";
import { EventController } from "./event.controller";

@Module({
  imports: [EventModuleBase],
  controllers: [EventController],
  providers: [EventService],
  exports: [EventService],
})
export class EventModule {}
