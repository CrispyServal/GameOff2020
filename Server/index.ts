import { mgobexsInterface } from './mgobexsInterface';

const gameServer: mgobexsInterface.GameServer.IGameServer = {
    mode: 'sync',
    onInitGameData: function (): mgobexsInterface.GameData {
        return {};
    },
    onRecvFromClient: function onRecvFromClient({ actionData, gameData, SDK, room, exports }: mgobexsInterface.ActionArgs<mgobexsInterface.UserDefinedData>) {
        gameData.pos = Math.floor(Math.random() * 2000);
        SDK.logger.debug('onRecvFromClient', gameData, actionData);
        setTimeout(() => {
            SDK.sendData({ playerIdList: [], data: { data: gameData, ts: new Date().toISOString() } }, { timeout: 2000, maxTry: 3 });
            SDK.exitAction();
        }, gameData.pos);
    },
    onJoinRoom: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onJoinRoom',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onCreateRoom: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onCreateRoom',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onLeaveRoom: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onLeaveRoom',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onRemovePlayer: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onRemovePlayer',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onDestroyRoom: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onDestroyRoom',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onChangeRoom: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onChangeRoom',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onChangeCustomPlayerStatus: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onChangeCustomPlayerStatus',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onChangePlayerNetworkState: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onChangePlayerNetworkState',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onStartFrameSync: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onStartFrameSync',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    },
    onStopFrameSync: function ({ actionData, gameData, SDK, room, exports }) {
        SDK.logger.debug(
            'onStopFrameSync',
            'actionData:', actionData,
            'gameData:', gameData,
            'room:', room
        );
    }
};

// 服务器初始化时调用
function onInitGameServer(tcb: any) {
    // 如需要，可以在此初始化 TCB
    const tcbApp = tcb.init({
        secretId: "AKIDMbhPzQ5socHm8LwK2Y0CEDxVptkLonz0",
        secretKey: "6yGXhmdSDk231jAKDLEqBSAj01nMgkuv",
        env: "TODO",
        serviceUrl: 'http://tcb-admin.tencentyun.com/admin',
        timeout: 5000,
    });

    console.log("em")
    console.log(tcbApp)

    // ...
}

export const mgobexsCode: mgobexsInterface.mgobexsCode = {
    logLevel: 'debug+',
    logLevelSDK: 'debug+',
    gameInfo: {
        gameId: "obg-j31fmxqp",
        serverKey: "fe04f6219e679f6ac97dba515780b898dba0a209",
    },
    onInitGameServer,
    gameServer
}
