import { Module, forwardRef } from "@nestjs/common";
import { AuthModule } from "../auth/auth.module";
import { DeviceModuleBase } from "./base/device.module.base";
import { DeviceService } from "./device.service";
import { DeviceController } from "./device.controller";
import { DeviceResolver } from "./device.resolver";

@Module({
  imports: [DeviceModuleBase, forwardRef(() => AuthModule)],
  controllers: [DeviceController],
  providers: [DeviceService, DeviceResolver],
  exports: [DeviceService],
})
export class DeviceModule {}
