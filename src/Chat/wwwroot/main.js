var leHub = $.connection.Chat;

var chatting = React.createClass({
	getInitialState: function () {
		leHub.on('receivedMessage', function (leMessage) {
			var messages = this.state.messages;
			messages.push(leMessage)
			this.setState({ messages: messages })
			console.log(this.state.messages);
			console.log('received message from bro :O')
		}.bind(this))

		return {
			messages: [],
			inputValue: ''
		}
	},
	handleInputChange: function (inputValue){
		this.setState({ inputValue: inputValue.target.value })
	},
	sendMessage: function () {
		$.connection.Chat.server.sendingAMessageToMyBro(this.state.inputValue)
		console.log('sent message to bro')
	},
	render: function () {
		return React.DOM.div(
			{ className: 'row' },
			this.state.messages.map(function (message) {
				return React.DOM.div({}, message)
			}),
			React.DOM.input({ className: 'col-sm-offset-4 col-sm-4', onChange: this.handleInputChange }),
			React.DOM.button({ className: 'col-sm-offset-4 col-sm-4 btn btn-success', onClick: this.sendMessage }, 'Send message')
		)
	}
})

var searching = React.createClass({
	getInitialState: function () {
		var leInterval = setInterval(function () {
			if(this.state.progress >= 100) clearInterval(leInterval)
			this.setState({ progress: this.state.progress + 10 })
		}.bind(this), 1200);

		leHub.client.foundABro = function (yourId) {
			console.log('your id: ' + yourId)
			clearInterval(leInterval)
			this.props.foundBro()
		}.bind(this)

		$.connection.hub.start().done(function () {
			leHub.server.aNewBroHasArrived('le message', 'le other message')
		})

		return {
			progress: 10
		}
	},
	render: function () {
		return React.DOM.div({ className: 'progress' }, 
			React.DOM.div({ className: 'progress-bar progress-bar-striped active', style: { width: this.state.progress + '%' } })
		)
	}
})

var chatButton = React.createClass({
	click: function () {
		this.props.searchForBro()
	},
	render: function () {
		return React.DOM.button({ className: 'btn btn-success', onClick: this.click }, 'Chat')
	}
})

var main = React.createClass({
	getInitialState: function () {
		return {
			state: 'home'
		}
	},
	searchForBro: function () {
		this.setState({ state: 'searchingForBro' })
	},
	foundBro: function () {
		this.setState({ state: 'chattingWithBro' })
	},
	submittingAnswer: function () {
		this.setState({ state: 'submittingAnswer' })
	},
	render: function () {
		if (this.state.state == 'home') return React.createElement(chatButton, { searchForBro: this.searchForBro })
		if (this.state.state == 'searchingForBro') return React.createElement(searching, { foundBro: this.foundBro })
		if (this.state.state == 'chattingWithBro') return React.createElement(chatting)
	}
})

React.render(React.createElement(main), $('.le')[0])