using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventEnum = WerewolfClient.WerewolfModel.EventEnum;
using CommandEnum = WerewolfClient.WerewolfCommand.CommandEnum;
using WerewolfAPI.Model;
using WMPLib;
using Role = WerewolfAPI.Model.Role;

namespace WerewolfClient
{
    public partial class MainForm : Form, View
    {
        private Timer _updateTimer;
        private WerewolfController controller;
        private Game.PeriodEnum _currentPeriod;
        private int _currentDay;
        private int _currentTime;
        private bool _voteActivated;
        private bool _actionActivated;
        private string _myRole;
        private bool _isDead;
        private List<Player> players = null;
        WindowsMediaPlayer room = new WindowsMediaPlayer();
        WindowsMediaPlayer ingame = new WindowsMediaPlayer();
        public MainForm()
        {
            InitializeComponent();
            room.URL = "room.mp3";
            ingame.URL = "ingame.mp3";
            room.controls.stop();
            ingame.controls.stop();
            foreach (int i in Enumerable.Range(0, 16))
            {
                this.Controls["GBPlayers"].Controls["BtnPlayer" + i].Click += new System.EventHandler(this.BtnPlayerX_Click);
                this.Controls["GBPlayers"].Controls["BtnPlayer" + i].Tag = i;
            }

            _updateTimer = new Timer();
            _voteActivated = false;
            _actionActivated = false;
            EnableButton(BtnJoin, true);
            EnableButton(BtnAction, false);
            EnableButton(BtnVote, false);
            _myRole = null;
            _isDead = false;
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.RequestUpdate;
            controller.ActionPerformed(wcmd);
        }

        public void AddChatMessage(string str)
        {
            TbChatBox.AppendText(str + Environment.NewLine);
        }

        public void EnableButton(Button btn, bool state)
        {
            btn.Visible = btn.Enabled = state;
        }

        private void UpdateAvatar(WerewolfModel wm)
        {
            int i = 0;
            foreach (Player player in wm.Players)
            {
                Controls["GBPlayers"].Controls["BtnPlayer" + i].Text = player.Name;
                if (player.Name == wm.Player.Name || player.Status != Player.StatusEnum.Alive)
                {
                    // FIXME, need to optimize this
                    Image img = Properties.Resources.villager_icon_resize_;
                    string role;
                    if (player.Name == wm.Player.Name)
                    {
                        role = _myRole;
                    }
                    else if (player.Role != null)
                    {
                        role = player.Role.Name;
                    }
                    else
                    {
                        continue;

                    }
                    switch (role)
                    {
                        case WerewolfModel.ROLE_SEER:
                            img = Properties.Resources.seer_icon_resize_;
                            Char_pic.Image = Properties.Resources.Seer_use;
                            Animation.Image = Properties.Resources.Seer2_standby;
                            break;
                        case WerewolfModel.ROLE_AURA_SEER:
                            img = Properties.Resources.auraseer_icon_resize_;
                            Char_pic.Image = Properties.Resources.AuraSeer_use;
                            Animation.Image = Properties.Resources.AuraSeer_standby;
                            break;
                        case WerewolfModel.ROLE_PRIEST:
                            img = Properties.Resources.bible_icon_resize_;
                            Char_pic.Image = Properties.Resources.Priest_use;
                            Animation.Image = Properties.Resources.Priest_standby;
                            break;
                        case WerewolfModel.ROLE_DOCTOR:
                            img = Properties.Resources.doctor_icon_resize_;
                            Char_pic.Image = Properties.Resources.Doctor_use;
                            Animation.Image = Properties.Resources.Doctor_standby;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF:
                            img = Properties.Resources.werewolf_icon_resize_;
                            Char_pic.Image = Properties.Resources.Werewolf_use;
                            Animation.Image = Properties.Resources.Werewolf_standby;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SEER:
                            img = Properties.Resources.werewolfseer_icon_resize_;
                            Char_pic.Image = Properties.Resources.WolfSeer_use;
                            Animation.Image = Properties.Resources.WolfSeer2_standby;
                            break;
                        case WerewolfModel.ROLE_ALPHA_WEREWOLF:
                            img = Properties.Resources.alphawerewolf_icon_resize_;
                            Char_pic.Image = Properties.Resources.AlphaWerewolf_use;
                            Animation.Image = Properties.Resources.AlphaWerewolf_standby;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                            img = Properties.Resources.werewolfshaman_icon_resize_;
                            Char_pic.Image = Properties.Resources.WolfShaman_use;
                            Animation.Image = Properties.Resources.WolfShaman_standby;
                            break;
                        case WerewolfModel.ROLE_MEDIUM:
                            img = Properties.Resources.medium_icon_resize_;
                            Char_pic.Image = Properties.Resources.Medium_use;
                            Animation.Image = Properties.Resources.Medium_standby; 
                            break;
                        case WerewolfModel.ROLE_BODYGUARD:
                            img = Properties.Resources.bodyguard_icon_resize_;
                            Char_pic.Image = Properties.Resources.Bodyguard_use;
                            Animation.Image = Properties.Resources.Bodyguard_standby;
                            break;
                        case WerewolfModel.ROLE_JAILER:
                            img = Properties.Resources.jailer_icon_resize_;
                            Char_pic.Image = Properties.Resources.Jailer_use;
                            Animation.Image = Properties.Resources.Jailer_standby;
                            break;
                        case WerewolfModel.ROLE_FOOL:
                            img = Properties.Resources.fool_icon_resize_;
                            Char_pic.Image = Properties.Resources.Fool_use;
                            Animation.Image = Properties.Resources.Fool_standby;
                            break;
                        case WerewolfModel.ROLE_HEAD_HUNTER:
                            img = Properties.Resources.bountyhunter_icon_resize_;
                            Char_pic.Image = Properties.Resources.Headhunter_use;
                            Animation.Image = Properties.Resources.Headhunter_standby;
                            break;
                        case WerewolfModel.ROLE_SERIAL_KILLER:
                            img = Properties.Resources.serialkiller_icon_resize_;
                            Char_pic.Image = Properties.Resources.SerialKiller_use;
                            Animation.Image = Properties.Resources.SerialKiller_standby;
                            break;
                        case WerewolfModel.ROLE_GUNNER:
                            img = Properties.Resources.gunner_icon_resize_;
                            Char_pic.Image = Properties.Resources.Gunner_use;
                            Animation.Image = Properties.Resources.Gunner_standby;
                            break;
                        case null:
                            img = Properties.Resources.Null_resize_;
                            Char_pic.Image = Properties.Resources.Null_resize_;
                            Animation.Image = Properties.Resources.Null_resize_;
                            break;
                    }
                    ((Button)Controls["GBPlayers"].Controls["BtnPlayer" + i]).Image = img;
                }
                i++;
            }
        }
        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case EventEnum.JoinGame:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            room.controls.play();
                            BtnJoin.Visible = false;
                            AddChatMessage("You're joing the game #" + wm.EventPayloads["Game.Id"] + ", please wait for game start.");
                            _updateTimer.Interval = 1000;
                            _updateTimer.Tick += new EventHandler(OnTimerEvent);
                            _updateTimer.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("You can't join the game, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case EventEnum.GameStopped:
                        ingame.controls.stop();
                        AddChatMessage("Game is finished, outcome is " + wm.EventPayloads["Game.Outcome"]);
                        _updateTimer.Enabled = false;
                        break;
                    case EventEnum.GameStarted:
                        room.controls.stop();
                        ingame.controls.play();
                        players = wm.Players;
                        _myRole = wm.EventPayloads["Player.Role.Name"];
                        AddChatMessage("Your role is " + _myRole + ".");
                        _currentPeriod = Game.PeriodEnum.Night;
                        EnableButton(BtnAction, true);
                        switch (_myRole)
                        {
                            case WerewolfModel.ROLE_PRIEST:
                                //BtnAction.Text = WerewolfModel.ACTION_HOLYWATER;
                                BtnAction.Image = Properties.Resources.น้ำมน;
                                break;
                            case WerewolfModel.ROLE_GUNNER:
                               // BtnAction.Text = WerewolfModel.ACTION_SHOOT;
                                BtnAction.Image = Properties.Resources.ยิง;
                                break;
                            case WerewolfModel.ROLE_JAILER:
                                //BtnAction.Text = WerewolfModel.ACTION_JAIL;
                                BtnAction.Image = Properties.Resources.ขัง;
                                break;
                            case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                                //BtnAction.Text = WerewolfModel.ACTION_ENCHANT;
                                BtnAction.Image = Properties.Resources.ใบ้;
                                break;
                            case WerewolfModel.ROLE_BODYGUARD:
                               // BtnAction.Text = WerewolfModel.ACTION_GUARD;
                                BtnAction.Image = Properties.Resources.คุ้มกัน;
                                break;
                            case WerewolfModel.ROLE_DOCTOR:
                                //BtnAction.Text = WerewolfModel.ACTION_HEAL;
                                BtnAction.Image = Properties.Resources.รักษา;
                                break;
                            case WerewolfModel.ROLE_SERIAL_KILLER:
                                //BtnAction.Text = WerewolfModel.ACTION_KILL;
                                BtnAction.Image = Properties.Resources.ฆ่า;
                                break;
                            case WerewolfModel.ROLE_SEER:
                            case WerewolfModel.ROLE_WEREWOLF_SEER:
                                //BtnAction.Text = WerewolfModel.ACTION_REVEAL;
                                BtnAction.Image = Properties.Resources.ตาหมา;
                                break;
                            case WerewolfModel.ROLE_AURA_SEER:
                                //BtnAction.Text = WerewolfModel.ACTION_AURA;
                                BtnAction.Image = Properties.Resources.เซีย;
                                break;
                            case WerewolfModel.ROLE_MEDIUM:
                                //BtnAction.Text = WerewolfModel.ACTION_REVIVE;
                                BtnAction.Image = Properties.Resources.ชุบ;
                                break;
                            default:
                                EnableButton(BtnAction, false);
                                break;
                        }
                        EnableButton(BtnVote, true);
                        EnableButton(BtnJoin, false);
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.SwitchToDayTime:
                        this.BackgroundImage = Properties.Resources.Dusk_resize_;
                        AddChatMessage("Switch to day time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Day;
                        LBPeriod.Text = "Day time of";
                        break;
                    case EventEnum.SwitchToNightTime:
                        this.BackgroundImage = Properties.Resources.WerewolfMoon_resize_;
                        AddChatMessage("Switch to night time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Night;
                        LBPeriod.Text = "Night time of";
                        break;
                    case EventEnum.UpdateDay:
                        // TODO  catch parse exception here
                        string tempDay = wm.EventPayloads["Game.Current.Day"];
                        _currentDay = int.Parse(tempDay);
                        LBDay.Text = tempDay;
                        break;
                    case EventEnum.UpdateTime:
                        string tempTime = wm.EventPayloads["Game.Current.Time"];
                        _currentTime = int.Parse(tempTime);
                        LBTime.Text = tempTime;
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.Vote:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your vote is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't vote now.");
                        }
                        break;
                    case EventEnum.Action:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your action is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't perform action now.");
                        }
                        break;
                    case EventEnum.YouShotDead:
                        AddChatMessage("You're shot dead by gunner.");
                        _isDead = true;
                        break;
                    case EventEnum.OtherShotDead:
                        AddChatMessage(wm.EventPayloads["Game.Target.Name"] + " was shot dead by gunner.");
                        break;
                    case EventEnum.Alive:
                        AddChatMessage(wm.EventPayloads["Game.Target.Name"] + " has been revived by medium.");
                        if (wm.EventPayloads["Game.Target.Id"] == null)
                        {
                            _isDead = false;
                        }
                        break;
                    case EventEnum.ChatMessage:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage(wm.EventPayloads["Game.Chatter"] + ":" + wm.EventPayloads["Game.ChatMessage"]);
                        }
                        break;
                    case EventEnum.Chat:
                        if (wm.EventPayloads["Success"] == WerewolfModel.FALSE)
                        {
                            switch (wm.EventPayloads["Error"])
                            {
                                case "403":
                                    AddChatMessage("You're not alive, can't talk now.");
                                    break;
                                case "404":
                                    AddChatMessage("You're not existed, can't talk now.");
                                    break;
                                case "405":
                                    AddChatMessage("You're not in a game, can't talk now.");
                                    break;
                                case "406":
                                    AddChatMessage("You're not allow to talk now, go to sleep.");
                                    break;
                            }
                        }
                        break;
                }
                // need to reset event
                wm.Event = EventEnum.NOP;
            }
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.JoinGame;
            controller.ActionPerformed(wcmd);
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnVote.BackColor = Color.Red;
            }
            _voteActivated = !_voteActivated;
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
                _actionActivated = false;
            }
        }
        private void BtnPlayerX_Click(object sender, EventArgs e)
        {
            Button btnPlayer = (Button)sender;
            int index = (int) btnPlayer.Tag;
            if (players == null)
            {
                // Nothing to do here;
                return;
            }
            if (_actionActivated)
            {
                _actionActivated = false;
             //   BtnAction.BackColor = Button.DefaultBackColor;
             //   AddChatMessage("You perform [" + BtnAction.Text + "] on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Action;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
            if (_voteActivated)
            {
                _voteActivated = false;
                BtnVote.BackColor = Button.DefaultBackColor;
                AddChatMessage("You vote on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Vote;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TbChatInput_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && TbChatInput.Text != "")
            {
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Chat;
                wcmd.Payloads = new Dictionary<string, string>() { { "Message", TbChatInput.Text } };
                TbChatInput.Text = "";
                controller.ActionPerformed(wcmd);
            }
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (_isDead)
            {
                this.Char_pic.Image = Properties.Resources.Null_resize_;
                this.Animation.Image = Properties.Resources.Null_resize_;
                AddChatMessage("You're dead!!");
                if (_myRole == WerewolfModel.ROLE_WEREWOLF) Animation.Image = Properties.Resources.Werewolf_died;
                if (_myRole == WerewolfModel.ROLE_ALPHA_WEREWOLF) Animation.Image = Properties.Resources.AlphaWerewolf_dead;
                if (_myRole == WerewolfModel.ROLE_BODYGUARD) Animation.Image = Properties.Resources.Bodyguard_died;
                if (_myRole == WerewolfModel.ROLE_DOCTOR) Animation.Image = Properties.Resources.Doctor_died;
                if (_myRole == WerewolfModel.ROLE_FOOL) Animation.Image = Properties.Resources.Fool_died;
                if (_myRole == WerewolfModel.ROLE_GUNNER) Animation.Image = Properties.Resources.Gunner_died;
                if (_myRole == WerewolfModel.ROLE_HEAD_HUNTER) Animation.Image = Properties.Resources.Headhunter_died;
                if (_myRole == WerewolfModel.ROLE_JAILER) Animation.Image = Properties.Resources.Jailer_died;
                if (_myRole == WerewolfModel.ROLE_MEDIUM) Animation.Image = Properties.Resources.Medium_died;
                if (_myRole == WerewolfModel.ROLE_PRIEST) Animation.Image = Properties.Resources.Priest_died;
                if (_myRole == WerewolfModel.ROLE_SEER) Animation.Image = Properties.Resources.Seer2_died;
                if (_myRole == WerewolfModel.ROLE_SERIAL_KILLER) Animation.Image = Properties.Resources.SerialKiller_died;
                if (_myRole == WerewolfModel.ROLE_WEREWOLF_SEER) Animation.Image = Properties.Resources.WolfSeer2_died;
                if (_myRole == WerewolfModel.ROLE_WEREWOLF_SHAMAN) Animation.Image = Properties.Resources.WolfShaman_died;
                return;
            }
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnAction.BackColor = Color.Red;
            }
            _actionActivated = !_actionActivated;
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
                _voteActivated = false;
            }
        }
    }
}
