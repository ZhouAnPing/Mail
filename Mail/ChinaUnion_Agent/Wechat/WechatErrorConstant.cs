﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_Agent.Wechat
{
    class WechatErrorConstant
    {
        public Hashtable WechatErrorHT = new Hashtable();
        public  WechatErrorConstant()
        {
            WechatErrorHT = new Hashtable();
            WechatErrorHT.Add("-1", "系统繁忙");
            WechatErrorHT.Add("0", "请求成功");
            WechatErrorHT.Add("40001", "获取access_token时Secret错误，或者access_token无效");
            WechatErrorHT.Add("40002", "不合法的凭证类型");
            WechatErrorHT.Add("40003", "不合法的UserID");
            WechatErrorHT.Add("40004", "不合法的媒体文件类型");
            WechatErrorHT.Add("40005", "不合法的文件类型");
            WechatErrorHT.Add("40006", "不合法的文件大小");
            WechatErrorHT.Add("40007", "不合法的媒体文件id");
            WechatErrorHT.Add("40008", "不合法的消息类型");
            WechatErrorHT.Add("40013", "不合法的corpid");
            WechatErrorHT.Add("40014", "不合法的access_token");
            WechatErrorHT.Add("40015", "不合法的菜单类型");
            WechatErrorHT.Add("40016", "不合法的按钮个数");
            WechatErrorHT.Add("40017", "不合法的按钮类型");
            WechatErrorHT.Add("40018", "不合法的按钮名字长度");
            WechatErrorHT.Add("40019", "不合法的按钮KEY长度");
            WechatErrorHT.Add("40020", "不合法的按钮URL长度");
            WechatErrorHT.Add("40021", "不合法的菜单版本号");
            WechatErrorHT.Add("40022", "不合法的子菜单级数");
            WechatErrorHT.Add("40023", "不合法的子菜单按钮个数");
            WechatErrorHT.Add("40024", "不合法的子菜单按钮类型");
            WechatErrorHT.Add("40025", "不合法的子菜单按钮名字长度");
            WechatErrorHT.Add("40026", "不合法的子菜单按钮KEY长度");
            WechatErrorHT.Add("40027", "不合法的子菜单按钮URL长度");
            WechatErrorHT.Add("40028", "不合法的自定义菜单使用员工");
            WechatErrorHT.Add("40029", "不合法的oauth_code");
            WechatErrorHT.Add("40031", "不合法的UserID列表");
            WechatErrorHT.Add("40032", "不合法的UserID列表长度");
            WechatErrorHT.Add("40033", "不合法的请求字符，不能包含\\uxxxx格式的字符");
            WechatErrorHT.Add("40035", "不合法的参数");
            WechatErrorHT.Add("40038", "不合法的请求格式");
            WechatErrorHT.Add("40039", "不合法的URL长度");
            WechatErrorHT.Add("40040", "不合法的插件token");
            WechatErrorHT.Add("40041", "不合法的插件id");
            WechatErrorHT.Add("40042", "不合法的插件会话");
            WechatErrorHT.Add("40048", "url中包含不合法domain");
            WechatErrorHT.Add("40054", "不合法的子菜单url域名");
            WechatErrorHT.Add("40055", "不合法的按钮url域名");
            WechatErrorHT.Add("40056", "不合法的agentid");
            WechatErrorHT.Add("40057", "不合法的callbackurl");
            WechatErrorHT.Add("40058", "不合法的红包参数");
            WechatErrorHT.Add("40059", "不合法的上报地理位置标志位");
            WechatErrorHT.Add("40060", "设置上报地理位置标志位时没有设置callbackurl");
            WechatErrorHT.Add("40061", "设置应用头像失败");
            WechatErrorHT.Add("40062", "不合法的应用模式");
            WechatErrorHT.Add("40063", "红包参数为空");
            WechatErrorHT.Add("40064", "管理组名字已存在");
            WechatErrorHT.Add("40065", "不合法的管理组名字长度");
            WechatErrorHT.Add("40066", "不合法的部门列表");
            WechatErrorHT.Add("40067", "标题长度不合法");
            WechatErrorHT.Add("40068", "不合法的标签ID");
            WechatErrorHT.Add("40069", "不合法的标签ID列表");
            WechatErrorHT.Add("40070", "列表中所有标签（用户）ID都不合法");
            WechatErrorHT.Add("40071", "不合法的标签名字，标签名字已经存在");
            WechatErrorHT.Add("40072", "不合法的标签名字长度");
            WechatErrorHT.Add("40073", "不合法的openid");
            WechatErrorHT.Add("40074", "news消息不支持指定为高保密消息");
            WechatErrorHT.Add("41001", "缺少access_token参数");
            WechatErrorHT.Add("41002", "缺少corpid参数");
            WechatErrorHT.Add("41003", "缺少refresh_token参数");
            WechatErrorHT.Add("41004", "缺少secret参数");
            WechatErrorHT.Add("41005", "缺少多媒体文件数据");
            WechatErrorHT.Add("41006", "缺少media_id参数");
            WechatErrorHT.Add("41007", "缺少子菜单数据");
            WechatErrorHT.Add("41008", "缺少oauth code");
            WechatErrorHT.Add("41009", "缺少UserID");
            WechatErrorHT.Add("41010", "缺少url");
            WechatErrorHT.Add("41011", "缺少agentid");
            WechatErrorHT.Add("41012", "缺少应用头像mediaid");
            WechatErrorHT.Add("41013", "缺少应用名字");
            WechatErrorHT.Add("41014", "缺少应用描述");
            WechatErrorHT.Add("41015", "缺少Content");
            WechatErrorHT.Add("41016", "缺少标题");
            WechatErrorHT.Add("41017", "缺少标签ID");
            WechatErrorHT.Add("41018", "缺少标签名字");
            WechatErrorHT.Add("42001", "access_token超时");
            WechatErrorHT.Add("42002", "refresh_token超时");
            WechatErrorHT.Add("42003", "oauth_code超时");
            WechatErrorHT.Add("42004", "插件token超时");
            WechatErrorHT.Add("43001", "需要GET请求");
            WechatErrorHT.Add("43002", "需要POST请求");
            WechatErrorHT.Add("43003", "需要HTTPS");
            WechatErrorHT.Add("43004", "需要接收者关注");
            WechatErrorHT.Add("43005", "需要好友关系");
            WechatErrorHT.Add("43006", "需要订阅");
            WechatErrorHT.Add("43007", "需要授权");
            WechatErrorHT.Add("43008", "需要支付授权");
            WechatErrorHT.Add("43009", "需要员工已关注");
            WechatErrorHT.Add("43010", "需要处于回调模式");
            WechatErrorHT.Add("43011", "需要企业授权");
            WechatErrorHT.Add("44001", "多媒体文件为空");
            WechatErrorHT.Add("44002", "POST的数据包为空");
            WechatErrorHT.Add("44003", "图文消息内容为空");
            WechatErrorHT.Add("44004", "文本消息内容为空");
            WechatErrorHT.Add("45001", "多媒体文件大小超过限制");
            WechatErrorHT.Add("45002", "消息内容超过限制");
            WechatErrorHT.Add("45003", "标题字段超过限制");
            WechatErrorHT.Add("45004", "描述字段超过限制");
            WechatErrorHT.Add("45005", "链接字段超过限制");
            WechatErrorHT.Add("45006", "图片链接字段超过限制");
            WechatErrorHT.Add("45007", "语音播放时间超过限制");
            WechatErrorHT.Add("45008", "图文消息超过限制");
            WechatErrorHT.Add("45009", "接口调用超过限制");
            WechatErrorHT.Add("45010", "创建菜单个数超过限制");
            WechatErrorHT.Add("45015", "回复时间超过限制");
            WechatErrorHT.Add("45016", "系统分组，不允许修改");
            WechatErrorHT.Add("45017", "分组名字过长");
            WechatErrorHT.Add("45018", "分组数量超过上限");
            WechatErrorHT.Add("45024", "账号数量超过上限");
            WechatErrorHT.Add("46001", "不存在媒体数据");
            WechatErrorHT.Add("46002", "不存在的菜单版本");
            WechatErrorHT.Add("46003", "不存在的菜单数据");
            WechatErrorHT.Add("46004", "不存在的员工");
            WechatErrorHT.Add("47001", "解析JSON/XML内容错误");
            WechatErrorHT.Add("48002", "Api禁用");
            WechatErrorHT.Add("50001", "redirect_uri未授权");
            WechatErrorHT.Add("50002", "员工不在权限范围");
            WechatErrorHT.Add("50003", "应用已停用");
            WechatErrorHT.Add("50004", "员工状态不正确（未关注状态）");
            WechatErrorHT.Add("50005", "企业已禁用");
            WechatErrorHT.Add("60001", "部门长度不符合限制");
            WechatErrorHT.Add("60002", "部门层级深度超过限制");
            WechatErrorHT.Add("60003", "部门不存在");
            WechatErrorHT.Add("60004", "父亲部门不存在");
            WechatErrorHT.Add("60005", "不允许删除有成员的部门");
            WechatErrorHT.Add("60006", "不允许删除有子部门的部门");
            WechatErrorHT.Add("60007", "不允许删除根部门");
            WechatErrorHT.Add("60008", "部门名称已存在");
            WechatErrorHT.Add("60009", "部门名称含有非法字符");
            WechatErrorHT.Add("60010", "部门存在循环关系");
            WechatErrorHT.Add("60011", "管理员权限不足，（user/department/agent）无权限");
            WechatErrorHT.Add("60012", "不允许删除默认应用");
            WechatErrorHT.Add("60013", "不允许关闭应用");
            WechatErrorHT.Add("60014", "不允许开启应用");
            WechatErrorHT.Add("60015", "不允许修改默认应用可见范围");
            WechatErrorHT.Add("60016", "不允许删除存在成员的标签");
            WechatErrorHT.Add("60017", "不允许设置企业");
            WechatErrorHT.Add("60102", "UserID已存在");
            WechatErrorHT.Add("60103", "手机号码不合法");
            WechatErrorHT.Add("60104", "手机号码已存在");
            WechatErrorHT.Add("60105", "邮箱不合法");
            WechatErrorHT.Add("60106", "邮箱已存在");
            WechatErrorHT.Add("60107", "微信号不合法");
            WechatErrorHT.Add("60108", "微信号已存在");
            WechatErrorHT.Add("60109", "QQ号已存在");
            WechatErrorHT.Add("60110", "部门个数超出限制");
            WechatErrorHT.Add("60111", "UserID不存在");
            WechatErrorHT.Add("60112", "成员姓名不合法,名字长度小于20个中文字");
            WechatErrorHT.Add("60113", "身份认证信息（微信号/手机/邮箱）不能同时为空");
            WechatErrorHT.Add("60114", "性别不合法");

        }
    }
}
